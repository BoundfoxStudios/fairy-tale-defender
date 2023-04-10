using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects.CallbackProcessors
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Input + "/Tooltip Actions")]
	public class TooltipActionsSO : ScriptableObject, GameInput.ITooltipsActions
	{
		public event InputReaderSO.ScreenPositionHandler Position = delegate { };

		public void OnTooltipPosition(InputAction.CallbackContext context)
		{
			if (!context.performed)
			{
				return;
			}

			Position(context.ReadValue<Vector2>());
		}
	}
}
