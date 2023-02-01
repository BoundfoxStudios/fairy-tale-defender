using System;
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

		private Vector2 _previousPanDelta = Vector2.zero;
		private int? _previousInputDeviceId;

		private bool CanProcessPan(int inputDeviceId) =>
			_previousInputDeviceId is null || _previousInputDeviceId == inputDeviceId;

		public void OnEdgePan(InputAction.CallbackContext context)
		{
			if (!context.performed || !Settings.Camera.EnableEdgePanning)
			{
				return;
			}

			ProcessPan(context);
		}

		public void OnCameraMovement(InputAction.CallbackContext context)
		{
			if (context.started || !Settings.Camera.EnableKeyboardPanning)
			{
				return;
			}

			ProcessPan(context);
		}

		private void ProcessPan(InputAction.CallbackContext context)
		{
			var inputDeviceId = context.control.device.deviceId;

			// Check, if we can process the pan based on the current device id.
			// This helps to prevent that moving the mouse overrides the keyboard movement and vice-versa.
			if (!CanProcessPan(inputDeviceId))
			{
				return;
			}

			var deltaMovement = context.ReadValue<Vector2>();

			if (deltaMovement == Vector2.zero && _previousPanDelta != Vector2.zero)
			{
				PanStop();
				_previousPanDelta = deltaMovement;
				_previousInputDeviceId = null;
				return;
			}

			if (deltaMovement == _previousPanDelta)
			{
				return;
			}

			Pan(deltaMovement);
			_previousPanDelta = deltaMovement;
			_previousInputDeviceId = inputDeviceId;
		}
	}
}
