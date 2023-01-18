using System;
using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoundfoxStudios.CommunityProject.Input.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Input + "/Input Reader")]
	public class InputReaderSO : ScriptableObject, GameInput.IGameplayActions, GameInput.IBuildSystemActions
	{
		[SerializeField]
		private BuildableEventChannelSO EnterBuildModeEventChannel;

		[SerializeField]
		private VoidEventChannelSO ExitBuildModeEventChannel;

		private GameInput _gameInput;

		public delegate void PositionHandler(Vector2 position);

		public PositionHandler BuildPosition = delegate { };
		public PositionHandler Build = delegate { };
		public Action BuildRotate = delegate { };

		private void OnEnable()
		{
			if (_gameInput == null)
			{
				_gameInput = new();

				_gameInput.Gameplay.SetCallbacks(this);
				_gameInput.BuildSystem.SetCallbacks(this);
			}

			EnterBuildModeEventChannel.Raised += EnterBuildMode;
			ExitBuildModeEventChannel.Raised += ExitBuildMode;
		}

		private void EnterBuildMode(BuildableEventChannelSO.EventArgs args)
		{
			EnableBuildSystemInput();
		}

		private void ExitBuildMode()
		{
			EnableGameplayInput();
		}

		private void OnDisable()
		{
			EnterBuildModeEventChannel.Raised -= EnterBuildMode;
			ExitBuildModeEventChannel.Raised -= ExitBuildMode;

			DisableAllInput();
		}

		public void DisableAllInput()
		{
			_gameInput.Gameplay.Disable();
			_gameInput.UI.Disable();
			_gameInput.BuildSystem.Disable();
		}

		private void EnableBuildSystemInput()
		{
			_gameInput.Gameplay.Disable();
			_gameInput.UI.Disable();
			_gameInput.BuildSystem.Enable();
		}

		private void EnableGameplayInput()
		{
			_gameInput.BuildSystem.Disable();
			_gameInput.Gameplay.Enable();
			_gameInput.UI.Enable();
		}

		public void OnBuildPosition(InputAction.CallbackContext context)
		{
			if (!context.performed)
			{
				return;
			}

			BuildPosition(context.ReadValue<Vector2>());
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

			BuildRotate();
		}
	}
}
