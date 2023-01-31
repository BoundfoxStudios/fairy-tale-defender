using System;
using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Extensions;
using BoundfoxStudios.CommunityProject.Infrastructure;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoundfoxStudios.CommunityProject.Input.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Input + "/Input Reader")]
	public class InputReaderSO : ScriptableObject, GameInput.IGameplayActions, GameInput.IBuildSystemActions
	{
		[field: SerializeField]
		private BuildableEventChannelSO EnterBuildModeEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO ExitBuildModeEventChannel { get; set; } = default!;

		private GameInput? _gameInput;
		private GameInput GameInput => _gameInput.EnsureOrThrow();

		public delegate void PositionHandler(Vector2 position);

		public event PositionHandler BuildPosition = delegate { };
		public event PositionHandler Build = delegate { };
		public event Action BuildRotate = delegate { };

		private void OnValidate()
		{
			Guard.AgainstNull(() => EnterBuildModeEventChannel, this);
			Guard.AgainstNull(() => ExitBuildModeEventChannel, this);
		}

		private void OnEnable()
		{
			if (_gameInput is null)
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
			GameInput.Gameplay.Disable();
			GameInput.UI.Disable();
			GameInput.BuildSystem.Disable();
		}

		private void EnableBuildSystemInput()
		{
			GameInput.Gameplay.Disable();
			GameInput.UI.Disable();
			GameInput.BuildSystem.Enable();
		}

		private void EnableGameplayInput()
		{
			GameInput.BuildSystem.Disable();
			GameInput.Gameplay.Enable();
			GameInput.UI.Enable();
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
