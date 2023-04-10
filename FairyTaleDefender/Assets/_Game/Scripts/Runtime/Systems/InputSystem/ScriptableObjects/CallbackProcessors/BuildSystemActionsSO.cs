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

		public void OnBuildPosition(InputAction.CallbackContext context)
		{
			if (!context.performed)
			{
				return;
			}

			Position(context.ReadValue<Vector2>());
		}

		public void OnBuild(InputAction.CallbackContext context)
		{
			if (!context.performed)
			{
				return;
			}

			Build(context.ReadValue<Vector2>());
		}

		public void OnBuildRotate(InputAction.CallbackContext context)
		{
			if (!context.performed)
			{
				return;
			}

			Rotate();
		}
	}
}
