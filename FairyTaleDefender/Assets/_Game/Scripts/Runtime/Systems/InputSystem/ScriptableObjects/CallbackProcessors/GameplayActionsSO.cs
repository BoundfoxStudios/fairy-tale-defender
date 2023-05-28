using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects.CallbackProcessors
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Input + "/Gameplay Actions")]
	public class GameplayActionsSO : ScriptableObject, GameInput.IGameplayActions
	{
		public event InputReaderSO.ScreenPositionHandler Click = delegate { };

		private Vector2 _lastPosition;

		public void OnClick(InputAction.CallbackContext context)
		{
			if (!context.canceled)
			{
				_lastPosition = context.ReadValue<Vector2>();
				return;
			}

			Click(_lastPosition);
		}
	}
}
