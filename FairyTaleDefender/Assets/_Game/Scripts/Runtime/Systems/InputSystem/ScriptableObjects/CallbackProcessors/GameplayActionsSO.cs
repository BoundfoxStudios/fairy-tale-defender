using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects.CallbackProcessors
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Input + "/Gameplay Actions")]
	public class GameplayActionsSO : ScriptableObject, GameInput.IGameplayActions
	{
		public event InputReaderSO.ScreenPositionHandler Click = delegate { };

		public void OnClick(InputAction.CallbackContext context)
		{
			if (!context.performed)
			{
				return;
			}

			Debug.Log("<color=red>Invoking click!</color>");
			Click(context.ReadValue<Vector2>());
		}
	}
}
