using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Extensions;
using BoundfoxStudios.CommunityProject.Input.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.BuildSystem
{
	public class BuildController : MonoBehaviour
	{
		[SerializeField]
		private LayerMask BuildableLayerMask;

		[SerializeField]
		private LayerMask ObstaclesLayerMask;

		[SerializeField]
		private Camera Camera;

		[SerializeField]
		private InputReaderSO InputReader;

		[SerializeField]
		private Material BlueprintMaterial;

		[SerializeField]
		private Material BlueprintInvalidMaterial;

		[Header("Listening Channels")]
		[SerializeField]
		private BuildableEventChannelSO EnterBuildModeEventChannel;

		[Header("Channels")]
		[SerializeField]
		private VoidEventChannelSO ExitBuildModeEventChannel;

		private BuildContext _buildContext;

		private class BuildContext
		{
			public IBuildable Buildable { get; set; }
			public GameObject BlueprintInstance { get; set; }
			public MeshRenderer[] MeshRenderers { get; set; }
			public Vector3 TilePosition { get; set; }
			public bool IsValidPosition { get; set; }
			public LayerMask PreviousLayerMask { get; set; }
		}

		private void OnEnable()
		{
			InputReader.BuildPosition += ReadBuildPosition;
			InputReader.Build += ReadBuild;
			EnterBuildModeEventChannel.Raised += EnterBuildMode;
			ExitBuildModeEventChannel.Raised += ExitBuildMode;
		}

		private void OnDisable()
		{
			InputReader.BuildPosition -= ReadBuildPosition;
			InputReader.Build -= ReadBuild;
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

			_buildContext = new()
			{
				Buildable = args.Buildable,
				BlueprintInstance = Instantiate(args.Buildable.BlueprintPrefab)
			};
			_buildContext.MeshRenderers = _buildContext.BlueprintInstance.GetComponentsInChildren<MeshRenderer>();
			_buildContext.BlueprintInstance.Deactivate();
		}

		private void ClearBuildContext()
		{
			if (_buildContext?.BlueprintInstance)
			{
				Destroy(_buildContext.BlueprintInstance);
			}

			_buildContext = null;
		}

		private void ReadBuild(Vector2 position)
		{
			// We ignore the position parameter here, so we do not have to raycast for the correct position again.
			if (_buildContext is { IsValidPosition: false })
			{
				return;
			}

			Destroy(_buildContext.BlueprintInstance);
			Instantiate(_buildContext.Buildable.Prefab, _buildContext.TilePosition, Quaternion.identity);
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
			var ray = Camera.ScreenPointToRay(position);

			if (!Physics.Raycast(ray, out var hitInfo, 1000, BuildableLayerMask | ObstaclesLayerMask))
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
			_buildContext.PreviousLayerMask = hitInfo.collider.gameObject.layer;
			if (Physics.Linecast(tilePosition + Vector3.down, tilePosition + Vector3.up, out _, ObstaclesLayerMask))
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
