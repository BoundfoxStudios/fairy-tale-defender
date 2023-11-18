using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects.CallbackProcessors
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Input + "/UI Effects Actions")]
	public class UIEffectsActionsSO : ScriptableObject, GameInput.IUIEffectsActions
	{
		public event InputReaderSO.ScreenPositionHandler Position = delegate { };

		public void OnPosition(InputAction.CallbackContext context)
		{
			if (!context.performed)
			{
				return;
			}

			Position(context.ReadValue<Vector2>());
		}
	}
}
