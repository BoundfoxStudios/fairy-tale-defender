using System;
using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects.CallbackProcessors
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Input + "/Build System Actions")]
	public class BuildSystemActionsSO : ScriptableObject, GameInput.IBuildSystemActions
	{
		public event InputReaderSO.ScreenPositionHandler Position = delegate { };
		public event InputReaderSO.ScreenPositionHandler Build = delegate { };
		public event Action Rotate = delegate { };
		public event Action Cancel = delegate { };

		private Vector2 _lastPosition;

		public void OnBuildPosition(InputAction.CallbackContext context)
		{
			if (!context.performed)
			{
				return;
			}

			_lastPosition = context.ReadValue<Vector2>();
			Position(_lastPosition);
		}

		public void OnBuild(InputAction.CallbackContext context)
		{
			if (!context.canceled)
			{
				return;
			}

			Build(_lastPosition);
		}

		public void OnBuildRotate(InputAction.CallbackContext context)
		{
			if (!context.performed)
			{
				return;
			}

			Rotate();
		}

		public void OnCancel(InputAction.CallbackContext context)
		{
			if (!context.performed)
			{
				return;
			}

			Cancel();
		}
	}
}
