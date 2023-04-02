using System.Threading;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.BuildSystem
{
	[AddComponentMenu(Constants.MenuNames.BuildSystem + "/" + nameof(BuildController))]
	public class BuildController : MonoBehaviour
	{
		[field: SerializeField]
		private LayerMask TerrainLayerMask { get; set; }

		[field: SerializeField]
		private LayerMask ObstaclesLayerMask { get; set; }

		[field: SerializeField]
		private CameraRuntimeAnchorSO CameraRuntimeAnchor { get; set; } = default!;

		[field: SerializeField]
		private InputReaderSO InputReader { get; set; } = default!;

		[field: SerializeField]
		private Material BlueprintMaterial { get; set; } = default!;

		[field: SerializeField]
		private Material BlueprintInvalidMaterial { get; set; } = default!;

		[field: SerializeField]
		private float TimeToRotate { get; set; } = 0.133f;

		[field: SerializeField]
		private WeaponRangePreview WeaponRangePreview { get; set; } = default!;

		[field: SerializeField]
		public TransformRuntimeAnchorSO LevelContainerRuntimeAnchor { get; private set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private BuildableEventChannelSO EnterBuildModeEventChannel { get; set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		public BuildableEventChannelSO BuiltEventChannel { get; private set; } = default!;

		[field: Header("Channels")]
		[field: SerializeField]
		private VoidEventChannelSO ExitBuildModeEventChannel { get; set; } = default!;

		private BuildContext? _buildContext;
		private LayerMask _terrainAndObstacleLayerMask;

		private class BuildContext
		{
			private Vector3 _tilePosition;

			public IAmBuildable Buildable { get; }
			public GameObject BlueprintInstance { get; }
			public MeshRenderer[] MeshRenderers { get; }

			public Vector3 TilePosition
			{
				get => _tilePosition;
				set
				{
					_tilePosition = value;
					BlueprintInstance.transform.position = value;
				}
			}

			public bool IsValidPosition { get; set; }
			public bool PreviousHasValidPosition { get; set; }
			public ICanCalculateEffectiveWeaponDefinition WeaponDefinition { get; }

			public BuildContext(IAmBuildable buildable)
			{
				Buildable = buildable;
				BlueprintInstance = Instantiate(buildable.BlueprintPrefab);
				MeshRenderers = BlueprintInstance.GetComponentsInChildren<MeshRenderer>();
				WeaponDefinition = Buildable.Prefab.GetComponentInChildren<ICanCalculateEffectiveWeaponDefinition>();
			}
		}

		private void Awake()
		{
			_terrainAndObstacleLayerMask = TerrainLayerMask | ObstaclesLayerMask;
		}

		private void OnEnable()
		{
			InputReader.BuildSystemActions.Position += ReadBuildPosition;
			InputReader.BuildSystemActions.Build += ReadBuild;
			InputReader.BuildSystemActions.Rotate += ReadBuildRotate;
			EnterBuildModeEventChannel.Raised += EnterBuildMode;
			ExitBuildModeEventChannel.Raised += ExitBuildMode;
		}

		private void OnDisable()
		{
			InputReader.BuildSystemActions.Position -= ReadBuildPosition;
			InputReader.BuildSystemActions.Build -= ReadBuild;
			InputReader.BuildSystemActions.Rotate -= ReadBuildRotate;
			EnterBuildModeEventChannel.Raised -= EnterBuildMode;
			ExitBuildModeEventChannel.Raised -= ExitBuildMode;
		}

		private void ExitBuildMode()
		{
			ClearBuildContext();
		}

		private void EnterBuildMode(BuildableEventChannelSO.EventArgs args)
		{
			ClearBuildContext();

			_buildContext = new(args.Buildable);
			_buildContext.BlueprintInstance.Deactivate();
		}

		private void ClearBuildContext()
		{
			if (_buildContext is not null && _buildContext.BlueprintInstance)
			{
				Destroy(_buildContext.BlueprintInstance);
			}

			_buildContext = null;
		}

		private void ReadBuild(Vector2 position)
		{
			// We ignore the position parameter here, so we do not have to raycast for the correct position again.
			if (_buildContext is null or { IsValidPosition: false })
			{
				return;
			}

			WeaponRangePreview.StopDisplayingWeaponRange();
			var rotation = _buildContext.BlueprintInstance.transform.rotation;
			Destroy(_buildContext.BlueprintInstance);
			Instantiate(_buildContext.Buildable.Prefab, _buildContext.TilePosition, rotation);

			BuiltEventChannel.Raise(new() { Buildable = _buildContext.Buildable });
			ExitBuildModeEventChannel.Raise();
		}

		private bool TryFindFromRay(Ray ray, out RaycastHit hit, LayerMask layerMask) =>
			Physics.Raycast(ray, out hit, 1000, layerMask);

		private void ReadBuildPosition(Vector2 screenPosition)
		{
			if (_buildContext == null)
			{
				return;
			}

			WeaponRangePreview.StopDisplayingWeaponRange();
			_buildContext.IsValidPosition = false;
			var blueprintInstance = _buildContext.BlueprintInstance;

			var ray = CameraRuntimeAnchor.ItemSafe.ScreenPointToRay(screenPosition);

			if (!TryFindFromRay(ray, out var terrainHit, TerrainLayerMask))
			{
				blueprintInstance.Deactivate();
				return;
			}

			blueprintInstance.Activate();

			// We're adjusting the position for the tile by 0.5f because the tile's pivot is at the center.
			var tilePosition = new Vector3(Mathf.Floor(terrainHit.point.x + 0.5f), terrainHit.collider.bounds.center.y,
				Mathf.Floor(terrainHit.point.z + 0.5f));

			_buildContext.TilePosition = tilePosition;

			// Adjust the casted box to be a bit smaller than a tile, otherwise we might hit a neighbor.
			Physics.BoxCast(tilePosition + Vector3.up * 20, Vector3.one * 0.45f, Vector3.down, out var terrainBoxHit,
				Quaternion.identity, 20, _terrainAndObstacleLayerMask);

			var hasValidPosition = terrainBoxHit.collider.gameObject.IsInLayerMask(TerrainLayerMask)
			                       && terrainBoxHit.collider.TryGetComponent<BuildInformation>(out var buildInformation)
			                       && buildInformation.IsBuildable;

			var needsMaterialSwap = _buildContext.PreviousHasValidPosition != hasValidPosition;
			_buildContext.PreviousHasValidPosition = hasValidPosition;

			if (needsMaterialSwap && hasValidPosition)
			{
				SwapMaterials(BlueprintInvalidMaterial, BlueprintMaterial, _buildContext.MeshRenderers);
			}
			else if (needsMaterialSwap && !hasValidPosition)
			{
				SwapMaterials(BlueprintMaterial, BlueprintInvalidMaterial, _buildContext.MeshRenderers);
				// We don't need to process further, because we can not build
				return;
			}

			if (!hasValidPosition)
			{
				return;
			}

			_buildContext.IsValidPosition = true;

			WeaponRangePreview.DisplayWeaponRange(new()
			{
				Transform = _buildContext.BlueprintInstance.transform,
				EffectiveWeaponDefinition = _buildContext.WeaponDefinition
			});
		}

		private void ReadBuildRotate()
		{
			if (_buildContext is null)
			{
				return;
			}

			var blueprintTransform = _buildContext.BlueprintInstance.transform;
			RotateBlueprintWithRangePreview(blueprintTransform, TimeToRotate, destroyCancellationToken);
		}

		private void RotateBlueprintWithRangePreview(Transform blueprintTransform, float timeToRotate,
			CancellationToken cancellationToken)
		{
			blueprintTransform.DOQuarterRotationAroundY(timeToRotate, cancellationToken);
			WeaponRangePreview.transform.DOQuarterRotationAroundY(timeToRotate, cancellationToken);
		}

		private void SwapMaterials(Material from, Material to, params MeshRenderer[] meshRenderers)
		{
			foreach (var meshRenderer in meshRenderers)
			{
				var sharedMaterials = meshRenderer.sharedMaterials;

				for (var i = 0; i < sharedMaterials.Length; i++)
				{
					var sharedMaterial = sharedMaterials[i];

					if (sharedMaterial == from)
					{
						sharedMaterials[i] = to;
					}
				}

				meshRenderer.sharedMaterials = sharedMaterials;
			}
		}
	}
}
