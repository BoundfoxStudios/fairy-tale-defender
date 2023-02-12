using BoundfoxStudios.CommunityProject.Extensions;
using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Systems.InputSystem.ScriptableObjects.CallbackProcessors;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Systems.InputSystem.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Input + "/Input Reader")]
	public class InputReaderSO : ScriptableObject
	{
		[field: SerializeField]
		private BuildableEventChannelSO EnterBuildModeEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO ExitBuildModeEventChannel { get; set; } = default!;

		[field: SerializeField]
		public BuildSystemActionsSO BuildSystemActions { get; private set; } = default!;

		[field: SerializeField]
		public GameplayActionsSO GameplayActions { get; private set; } = default!;

		private GameInput? _gameInput;
		private GameInput GameInput => _gameInput.EnsureOrThrow();

		public delegate void ScreenPositionHandler(Vector2 position);

		public delegate void DeltaHandler(Vector2 delta);

		private void OnEnable()
		{
			if (_gameInput is null)
			{
				_gameInput = new();

				_gameInput.Gameplay.SetCallbacks(GameplayActions);
				_gameInput.BuildSystem.SetCallbacks(BuildSystemActions);
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
	}
}
