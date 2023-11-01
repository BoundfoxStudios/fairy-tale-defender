using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.TooltipSystem
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(TooltipController))]
	public class TooltipController : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private TextTooltipDisplay TextTooltipDisplay { get; set; } = default!;

		[field: SerializeField]
		private BuildTowerTooltipDisplay BuildTowerTooltipDisplay { get; set; } = default!;


		[field: SerializeField]
		private GameObject[] TooltipContainers { get; set; } = default!;

		[field: SerializeField]
		private InputReaderSO InputReader { get; set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private TooltipEventChannelSO ShowTooltipEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO HideTooltipEventChannel { get; set; } = default!;

		[field: SerializeField]
		private LoadSceneEventChannelSO LoadSceneEventChannel { get; set; } = default!;

		private TooltipDisplay? _activeTooltipDisplay;

		private void Awake()
		{
			foreach (var tooltipContainer in TooltipContainers)
			{
				tooltipContainer.SetActive(false);
			}
		}

		private void OnEnable()
		{
			ShowTooltipEventChannel.Raised += ShowTooltip;
			HideTooltipEventChannel.Raised += HideTooltip;
			LoadSceneEventChannel.Raised += HideTooltipOnSceneChange;

			InputReader.TooltipActions.Position += SetTooltipTextPosition;
		}

		private void HideTooltipOnSceneChange(LoadSceneEventChannelSO.EventArgs _)
		{
			HideTooltip();
		}

		private void OnDisable()
		{
			ShowTooltipEventChannel.Raised -= ShowTooltip;
			HideTooltipEventChannel.Raised -= HideTooltip;
			LoadSceneEventChannel.Raised -= HideTooltipOnSceneChange;

			InputReader.TooltipActions.Position -= SetTooltipTextPosition;
		}

		private void ShowTooltip(TooltipEventChannelSO.EventArgs args)
		{
			var display = ResolveDisplay(args.Tooltip);

			if (display is null)
			{
				throw new("Did not get a display to display the tooltip!");
			}

			_activeTooltipDisplay = display;
			_activeTooltipDisplay.Show(args.Tooltip, args.ScreenPosition);
		}

		private void SetTooltipTextPosition(Vector2 position)
		{
			if (_activeTooltipDisplay is null)
			{
				return;
			}

			_activeTooltipDisplay.SetPosition(position);
		}

		private void HideTooltip()
		{
			if (_activeTooltipDisplay is null)
			{
				return;
			}

			_activeTooltipDisplay.Hide();
			_activeTooltipDisplay = null;
		}

		private TooltipDisplay ResolveDisplay<T>(T tooltip)
			where T : class, ITooltip => tooltip switch
			{
				ITextTooltip => TextTooltipDisplay,
				IBuildTowerTooltip => BuildTowerTooltipDisplay,
				_ => throw new($"{typeof(T)} is not implemented yet.")
			};
	}
}
