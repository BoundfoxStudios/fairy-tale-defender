using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.InputSystem
{
	[AddComponentMenu(Constants.MenuNames.Input + "/" + nameof(SelectionController))]
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
		public VoidEventChannelSO WeaponDeselectedEventChannel { get; private set; } = default!;

		[field: SerializeField]
		private LayerMask TowerLayerMask { get; set; }

		private Transform? _currentSelection;

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
			if (!CameraRuntimeAnchor.Item)
			{
				return;
			}

			var ray = CameraRuntimeAnchor.ItemSafe.ScreenPointToRay(position);

			if (!Physics.Raycast(ray, out var hitInfo, 1000, TowerLayerMask))
			{
				if (_currentSelection is not null)
				{
					WeaponDeselectedEventChannel.Raise();
					_currentSelection = null;
				}

				return;
			}

			// Prevent selecting the same transform multiple times
			if (hitInfo.transform == _currentSelection)
			{
				return;
			}

			_currentSelection = hitInfo.transform;
			var canCalculateWeaponDefinition = _currentSelection.GetComponentInChildren<ICanCalculateEffectiveWeaponDefinition>();
			WeaponSelectedEventChannel.Raise(new()
			{
				Transform = _currentSelection,
				EffectiveWeaponDefinition = canCalculateWeaponDefinition
			});
		}
	}
}
