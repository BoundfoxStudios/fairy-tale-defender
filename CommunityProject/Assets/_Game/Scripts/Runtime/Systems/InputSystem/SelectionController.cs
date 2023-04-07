using BoundfoxStudios.CommunityProject.Entities.Weapons;
using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Systems.InputSystem.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Systems.InputSystem
{
	public class SelectionController : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private InputReaderSO InputReader { get; set; } = default!;

		[field: SerializeField]
		private CameraRuntimeAnchorSO CameraRuntimeAnchor { get; set; } = default!;

		[field: Header("Broadcasting Event Channels")]
		[field: SerializeField]
		public WeaponSelectedEventChannelSO WeaponSelectedEventChannel { get; private set; } = default!;

		[field: SerializeField]
		public VoidEventChannelSO WeaponDeselectEventChannel { get; private set; } = default!;

		[field: SerializeField]
		private LayerMask TowerLayerMask { get; set; }

		private void OnEnable()
		{
			InputReader.GameplayActions.Click += GameplayActionsOnClick;
		}

		private void OnDisable()
		{
			InputReader.GameplayActions.Click -= GameplayActionsOnClick;
		}

		private void GameplayActionsOnClick(Vector2 position)
		{
			var ray = CameraRuntimeAnchor.ItemSafe.ScreenPointToRay(position);

			if (!Physics.Raycast(ray, out var hitInfo, 1000, TowerLayerMask))
			{
				WeaponDeselectEventChannel.Raise();
				return;
			}

			var hitTransform = hitInfo.transform;
			var canCalculateWeaponDefinition = hitTransform.GetComponentInChildren<ICanCalculateWeaponDefinition>();
			WeaponSelectedEventChannel.Raise(new()
			{
				Transform = hitTransform,
				WeaponDefinition = canCalculateWeaponDefinition
			});
		}
	}
}
