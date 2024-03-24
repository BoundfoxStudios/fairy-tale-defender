using System.Threading;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects;
using DG.Tweening;
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
		private GameObject? BuildEffect { get; set; }

		[field: SerializeField]
		private float YBuildOffset { get; set; } = 0.2f;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private BuildableEventChannelSO EnterBuildModeEventChannel { get; set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		public BuildableEventChannelSO BuiltEventChannel { get; private set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO ExitBuildModeEventChannel { get; set; } = default!;

		private BuildContext? _buildContext;
		private LayerMask _terrainAndObstacleLayerMask;

		private class BuildContext
		{
			private Vector3 _tilePosition;
			private readonly float _yOffset;

			public IAmBuildable Buildable { get; }
			public GameObject BlueprintInstance { get; }
			public MeshRenderer[] MeshRenderers { get; }

			public Vector3 TilePosition
			{
				get => _tilePosition;
				set
				{
					var offsetPosition = value + new Vector3(0, _yOffset, 0);
					_tilePosition = offsetPosition;
					BlueprintInstance.transform.position = offsetPosition;
				}
			}

			public bool IsValidPosition { get; set; }
			public bool? PreviousHasValidPosition { get; set; }
			public ICanCalculateEffectiveWeaponDefinition WeaponDefinition { get; }

			public BuildContext(IAmBuildable buildable, float yOffset)
			{
				_yOffset = yOffset;
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
			InputReader.BuildSystemActions.Cancel += ExitBuildMode;
			EnterBuildModeEventChannel.Raised += EnterBuildMode;
		}

		private void OnDisable()
		{
			InputReader.BuildSystemActions.Position -= ReadBuildPosition;
			InputReader.BuildSystemActions.Build -= ReadBuild;
			InputReader.BuildSystemActions.Rotate -= ReadBuildRotate;
			InputReader.BuildSystemActions.Cancel -= ExitBuildMode;
			EnterBuildModeEventChannel.Raised -= EnterBuildMode;
		}

		private void ExitBuildMode()
		{
			ClearBuildContext();
			WeaponRangePreview.StopDisplayingWeaponRange();
			ExitBuildModeEventChannel.Raise();
		}

		private void EnterBuildMode(BuildableEventChannelSO.EventArgs args)
		{
			ClearBuildContext();

			_buildContext = new(args.Buildable, YBuildOffset);
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

			_buildContext.BlueprintInstance.transform.DOComplete();
			WeaponRangePreview.StopDisplayingWeaponRange();
			var rotation = _buildContext.BlueprintInstance.transform.rotation;
			Destroy(_buildContext.BlueprintInstance);
			Instantiate(_buildContext.Buildable.Prefab, _buildContext.TilePosition, rotation);

			if (BuildEffect)
			{
				Instantiate(BuildEffect, _buildContext.TilePosition, Quaternion.identity);
			}

			BuiltEventChannel.Raise(new() { Buildable = _buildContext.Buildable });
			ExitBuildMode();
		}

		private bool TryFindFromRay(Ray ray, out RaycastHit hit, LayerMask layerMask) =>
			Physics.Raycast(ray, out hit, 1000, layerMask);

		private void ReadBuildPosition(Vector2 screenPosition)
		{
			if (_buildContext == null)
			{
				return;
			}

			// This method is the algorithm for placing a tower in the game.

			// 1. Assume we have a invalid position.
			WeaponRangePreview.StopDisplayingWeaponRange();
			_buildContext.IsValidPosition = false;
			var blueprintInstance = _buildContext.BlueprintInstance;

			// 2. Check, if we hit any terrain in the scene. If not, turn off the blueprint.
			var ray = CameraRuntimeAnchor.ItemSafe.ScreenPointToRay(screenPosition);

			if (!TryFindFromRay(ray, out var terrainHit, TerrainLayerMask))
			{
				blueprintInstance.Deactivate();
				return;
			}

			blueprintInstance.Activate();

			// This threshold is used for casting and determine positions.
			// For casting: we adjust any casting box to not overlap with other terrain tile colliders.
			// For positions: to get the tile-center we need to add 0.5f. However that will lead to getting the wrong tile
			//   if the mouse cursor hovers over a wall of a tile. To counter that, we place it slightly off-center, so we
			//   find the tile of the wall and not its neighbour. For the final calculation we'll add 0.5f later to get
			//   the real center.
			const float threshold = 0.49f;
			const float centerAdjustment = 0.5f;
			const int boxCastHeight = 20;

			// 3. From the raycast position, create a new one that is in the middle of the hit terrain tile.
			var tilePosition = new Vector3(Mathf.Floor(terrainHit.point.x + threshold), terrainHit.point.y,
				Mathf.Floor(terrainHit.point.z + threshold));

			// 4. Make a box cast from above the tile down to the tile, also hitting anything on the ObstacleLayerMask.
			// We need it to check, if there is an obstacle placed on the tile.
			// Adjust the casted box to be a bit smaller than a tile, otherwise we might hit a neighbor.
			var hitsTerrainOrObstacle = Physics.BoxCast(tilePosition + Vector3.up * boxCastHeight, Vector3.one * threshold,
				Vector3.down,
				out var terrainWithObstaclesBoxHit, Quaternion.identity, boxCastHeight, _terrainAndObstacleLayerMask);

			// 5. Additionally, cast one more box cast only hitting the TerrainLayerMask to determine the correct position
			// to place the tower.
			var hitsTerrain = Physics.BoxCast(tilePosition + Vector3.up * boxCastHeight, Vector3.one * threshold,
				Vector3.down,
				out var terrainBoxHit, Quaternion.identity, boxCastHeight, TerrainLayerMask);

			// It could be, that we do not hit anything if we cast exactly in a gap of colliders or on some collider walls.
			// In that case we just exit here and hide the blueprint.
			if (!hitsTerrainOrObstacle || !hitsTerrain)
			{
				blueprintInstance.Deactivate();
				return;
			}

			// 6. From the hit terrain, determine the real position for the blueprint.
			// For the final position, aka the tile's center, we add 0.5f.
			tilePosition = new(Mathf.Floor(terrainBoxHit.point.x + centerAdjustment), terrainBoxHit.collider.bounds.center.y,
				Mathf.Floor(terrainBoxHit.point.z + centerAdjustment));
			_buildContext.TilePosition = tilePosition;

			// 7. Determine if we have a valid position. A valid positions means:
			//   - The BoxCast only hits Terrain -> no obstacle is on top of it
			//   - The hit TerrainTile needs a BuildInformation script. That script knows, if the tile is buildable or not.
			//   - If we got a BuildInformation script, we check, if it is buildable.
			var hasValidPosition = terrainWithObstaclesBoxHit.collider.gameObject.IsInLayerMask(TerrainLayerMask)
			                       && terrainWithObstaclesBoxHit.collider
				                       .TryGetComponent<BuildInformation>(out var buildInformation)
			                       && buildInformation.IsBuildable;

			// 8. Check, if we need to swap the material if we change from a valid to an invalid position and vice-versa.
			var needsMaterialSwap = _buildContext.PreviousHasValidPosition is null
			                        || _buildContext.PreviousHasValidPosition.Value != hasValidPosition;
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
