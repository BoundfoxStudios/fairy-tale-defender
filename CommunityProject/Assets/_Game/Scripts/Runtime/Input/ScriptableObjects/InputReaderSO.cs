using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Input.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Input + "/Input Reader")]
	public class InputReaderSO : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions
	{
		private GameInput _gameInput;

		private void OnEnable()
		{
			if (_gameInput == null)
			{
				_gameInput = new GameInput();

				_gameInput.Gameplay.SetCallbacks(this);
				_gameInput.UI.SetCallbacks(this);
			}
		}

		private void OnDisable()
		{
			DisableAllInput();
		}

		public void DisableAllInput()
		{
			_gameInput.Gameplay.Disable();
			_gameInput.UI.Disable();
		}

		public void EnableGameplayInput()
		{
			_gameInput.UI.Disable();
			_gameInput.Gameplay.Enable();
		}

		public void EnableUIInput()
		{
			_gameInput.Gameplay.Disable();
			_gameInput.UI.Enable();
		}
	}
}
