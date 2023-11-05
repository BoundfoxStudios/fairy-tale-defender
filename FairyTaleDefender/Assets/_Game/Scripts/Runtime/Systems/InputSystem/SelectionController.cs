using System.Collections.Generic;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Buildings.Towers;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

		[field: SerializeField]
		private GraphicRaycaster GraphicRaycaster { get; set; } = default!;

		private Transform? _currentSelection;

		private List<RaycastResult> _uiOverGameObjectResultCache = new(1);

		private void OnEnable()
		{
			InputReader.GameplayActions.Click += GameplayActionsOnClick;
		}

		private void OnDisable()
		{
			InputReader.GameplayActions.Click -= GameplayActionsOnClick;
		}

		private bool IsPointerOverGameObject(Vector2 position)
		{
			var pointerEventData = new PointerEventData(EventSystem.current)
			{
				position = position
			};

			_uiOverGameObjectResultCache.Clear();
			GraphicRaycaster.Raycast(pointerEventData, _uiOverGameObjectResultCache);

			return _uiOverGameObjectResultCache.Count > 0;
		}

		private void GameplayActionsOnClick(Vector2 position)
		{
			if (!CameraRuntimeAnchor.Item || IsPointerOverGameObject(position))
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
			var canCalculateWeaponDefinition =
				_currentSelection.GetComponentInChildren<ICanCalculateEffectiveWeaponDefinition>();
			WeaponSelectedEventChannel.Raise(new()
			{
				Transform = _currentSelection,
				EffectiveWeaponDefinition = canCalculateWeaponDefinition,
				Tower = _currentSelection.GetComponent<Tower>().TowerDefinition,
			});
		}
	}
}
