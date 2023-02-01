using System;
using BoundfoxStudios.CommunityProject.Infrastructure;
using BoundfoxStudios.CommunityProject.Settings.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoundfoxStudios.CommunityProject.Input.ScriptableObjects.CallbackProcessors
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Input + "/Gameplay Actions")]
	public class GameplayActionsSO : ScriptableObject, GameInput.IGameplayActions
	{
		[field: SerializeField]
		private SettingsSO Settings { get; set; } = default!;

		public event InputReaderSO.DeltaHandler Pan = delegate { };
		public event Action PanStop = delegate { };

		private bool _isPanning;
		private Vector2 _previousPanDelta = Vector2.zero;

		private void Awake()
		{
			Guard.AgainstNull(() => Settings, this);
		}

		public void OnEdgePan(InputAction.CallbackContext context)
		{
			if (!context.performed || !Settings.Camera.EnableEdgePanning)
			{
				return;
			}

			ProcessPan(context.ReadValue<Vector2>());
		}

		public void OnCameraMovement(InputAction.CallbackContext context)
		{
			if (context.started || !Settings.Camera.EnableKeyboardPanning)
			{
				return;
			}

			ProcessPan(context.ReadValue<Vector2>());
		}

		private void ProcessPan(Vector2 deltaMovement)
		{
			if (deltaMovement == Vector2.zero && _previousPanDelta != Vector2.zero)
			{
				PanStop();
				_previousPanDelta = deltaMovement;
				return;
			}

			if (deltaMovement != _previousPanDelta)
			{
				Pan(deltaMovement);
			}

			_previousPanDelta = deltaMovement;
		}
	}
}
