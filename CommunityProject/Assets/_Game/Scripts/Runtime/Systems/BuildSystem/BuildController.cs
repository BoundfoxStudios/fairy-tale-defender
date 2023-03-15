using BoundfoxStudios.CommunityProject.Extensions;
using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Systems.InputSystem.ScriptableObjects;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Systems.BuildSystem
{
	public class BuildController : MonoBehaviour
	{
		[field: SerializeField]
		private LayerMask BuildableLayerMask { get; set; }

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
		private LayerMask _buildableAndObstacleLayerMask;

		private class BuildContext
		{
			public IAmBuildable Buildable { get; }
			public GameObject BlueprintInstance { get; }
			public MeshRenderer[] MeshRenderers { get; }
			public Vector3 TilePosition { get; set; }
			public bool IsValidPosition { get; set; }
			public LayerMask PreviousLayerMask { get; set; }

			public BuildContext(IAmBuildable buildable)
			{
				Buildable = buildable;
				BlueprintInstance = Instantiate(buildable.BlueprintPrefab);
				MeshRenderers = BlueprintInstance.GetComponentsInChildren<MeshRenderer>();
			}
		}

		private void Awake()
		{
			_buildableAndObstacleLayerMask = BuildableLayerMask | ObstaclesLayerMask;
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

			var rotation = _buildContext.BlueprintInstance.transform.rotation;
			Destroy(_buildContext.BlueprintInstance);
			Instantiate(_buildContext.Buildable.Prefab, _buildContext.TilePosition, rotation);

			BuiltEventChannel.Raise(new() { Buildable = _buildContext.Buildable });
			ExitBuildModeEventChannel.Raise();
		}

		private void ReadBuildPosition(Vector2 position)
		{
			if (_buildContext == null)
			{
				return;
			}

			_buildContext.IsValidPosition = false;
			var blueprintInstance = _buildContext.BlueprintInstance;
			var ray = CameraRuntimeAnchor.ItemSafe.ScreenPointToRay(position);

			if (!Physics.Raycast(ray, out var hitInfo, 1000, _buildableAndObstacleLayerMask))
			{
				blueprintInstance.Deactivate();
				return;
			}

			blueprintInstance.Activate();
			var tilePosition = hitInfo.collider.transform.position;
			_buildContext.TilePosition = tilePosition;
			_buildContext.BlueprintInstance.transform.position = tilePosition;

			var layerMask = hitInfo.collider.gameObject.layer;
			var needsMaterialSwap = _buildContext.PreviousLayerMask != layerMask;
			_buildContext.PreviousLayerMask = layerMask;

			// We create a line cast that casts from 10 units above down to the tile position, so we can hit a collider's
			// front face. No Tower will be 10 units tall, so that should be safe (famous last words...:))
			if (Physics.Linecast(tilePosition + Vector3.up * 10, tilePosition + Vector3.down, out _, ObstaclesLayerMask))
			{
				if (needsMaterialSwap)
				{
					SwapMaterials(BlueprintMaterial, BlueprintInvalidMaterial, _buildContext.MeshRenderers);
				}

				return;
			}

			if (needsMaterialSwap)
			{
				SwapMaterials(BlueprintInvalidMaterial, BlueprintMaterial, _buildContext.MeshRenderers);
			}

			_buildContext.IsValidPosition = true;
		}

		private void ReadBuildRotate()
		{
			if (_buildContext is null)
			{
				return;
			}

			var blueprintTransform = _buildContext.BlueprintInstance.transform;
			blueprintTransform.DOComplete();
			blueprintTransform
				.DOLocalRotate(blueprintTransform.rotation.eulerAngles + new Vector3(0, 90, 0), TimeToRotate,
					RotateMode.FastBeyond360)
				.WithCancellation(destroyCancellationToken);
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
