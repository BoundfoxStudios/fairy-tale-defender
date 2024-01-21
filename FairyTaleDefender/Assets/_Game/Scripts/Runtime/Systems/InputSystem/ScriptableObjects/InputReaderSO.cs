using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects.CallbackProcessors;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Input + "/Input Reader")]
	public class InputReaderSO : ScriptableObject
	{
		[field: Header("References")]
		[field: SerializeField]
		public BuildSystemActionsSO BuildSystemActions { get; private set; } = default!;

		[field: SerializeField]
		public GameplayActionsSO GameplayActions { get; private set; } = default!;

		[field: SerializeField]
		public TooltipActionsSO TooltipActions { get; private set; } = default!;

		[field: SerializeField]
		public CameraActionsSO CameraActions { get; private set; } = default!;

		[field: SerializeField]
		public UIEffectsActionsSO UIEffectsActions { get; private set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private BuildableEventChannelSO EnterBuildModeEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO ExitBuildModeEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO GameplayStartEventChannel { get; set; } = default!;

		[field: SerializeField]
		private TooltipEventChannelSO ShowTooltipEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO HideTooltipEventChannel { get; set; } = default!;

		[field: SerializeField]
		private LoadSceneEventChannelSO LoadSceneEventChannel { get; set; } = default!;

		private GameInput? _gameInput;
		private GameInput GameInput => _gameInput.EnsureOrThrow();

		public delegate void ScreenPositionHandler(Vector2 screenPosition);

		public delegate void DeltaHandler(Vector2 delta);

		private void OnEnable()
		{
			if (_gameInput is null)
			{
				_gameInput = new();

				_gameInput.Gameplay.SetCallbacks(GameplayActions);
				_gameInput.BuildSystem.SetCallbacks(BuildSystemActions);
				_gameInput.Tooltips.SetCallbacks(TooltipActions);
				_gameInput.Camera.SetCallbacks(CameraActions);
				_gameInput.UIEffects.SetCallbacks(UIEffectsActions);
			}

			_gameInput.UIEffects.Enable();

			GameplayStartEventChannel.Raised += GameplayStart;
			EnterBuildModeEventChannel.Raised += EnterBuildMode;
			ExitBuildModeEventChannel.Raised += ExitBuildMode;
			ShowTooltipEventChannel.Raised += ShowTooltip;
			HideTooltipEventChannel.Raised += HideTooltip;
			LoadSceneEventChannel.Raised += DisableAllInputInLoadScreen;
		}

		private void HideTooltip()
		{
			DisableTooltipInput();
		}

		private void ShowTooltip(TooltipEventChannelSO.EventArgs args)
		{
			EnableTooltipInput();
		}

		private void EnterBuildMode(BuildableEventChannelSO.EventArgs args)
		{
			EnableBuildSystemInput();
		}

		private void ExitBuildMode()
		{
			GameInput.BuildSystem.Disable();
			EnableGameplayInput();
		}

		private void OnDisable()
		{
			EnterBuildModeEventChannel.Raised -= EnterBuildMode;
			ExitBuildModeEventChannel.Raised -= ExitBuildMode;
			GameplayStartEventChannel.Raised -= GameplayStart;
			ShowTooltipEventChannel.Raised -= ShowTooltip;
			HideTooltipEventChannel.Raised -= HideTooltip;
			LoadSceneEventChannel.Raised -= DisableAllInputInLoadScreen;

			DisableAllInput();
		}

		private void GameplayStart()
		{
			EnableGameplayInputDelayed();
		}

		private void DisableAllInput()
		{
			GameInput.Gameplay.Disable();
			GameInput.UI.Disable();
			GameInput.BuildSystem.Disable();
			GameInput.Tooltips.Disable();
			GameInput.Camera.Disable();
		}

		private void DisableAllInputInLoadScreen(LoadSceneEventChannelSO.EventArgs args)
		{
			if (!args.ShowLoadingScreen)
			{
				return;
			}
			DisableAllInput();
		}

		private void EnableBuildSystemInput()
		{
			GameInput.Gameplay.Disable();
			GameInput.UI.Disable();
			GameInput.BuildSystem.Enable();
			GameInput.Camera.Enable();
		}

		private void EnableGameplayInputDelayed()
		{
			GameInput.BuildSystem.Disable();
			EnableGameplayInput();
		}

		private void EnableGameplayInput()
		{
			GameInput.Gameplay.Enable();
			GameInput.UI.Enable();
			GameInput.Camera.Enable();
		}

		private void DisableTooltipInput()
		{
			GameInput.Tooltips.Disable();
		}

		private void EnableTooltipInput()
		{
			GameInput.Tooltips.Enable();
		}
	}
}
