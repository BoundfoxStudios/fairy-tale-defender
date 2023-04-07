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
		public TextTooltipDisplay TextTooltipDisplay { get; private set; } = default!;

		[field: SerializeField]
		public GameObject TooltipContainer { get; private set; } = default!;

		[field: SerializeField]
		public InputReaderSO InputReader { get; private set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		public TooltipEventChannelSO ShowTooltipEventChannel { get; private set; } = default!;

		[field: SerializeField]
		public VoidEventChannelSO HideTooltipEventChannel { get; private set; } = default!;

		private TooltipDisplay? _activeTooltipDisplay;

		private void Awake()
		{
			TooltipContainer.SetActive(false);
		}

		private void OnEnable()
		{
			ShowTooltipEventChannel.Raised += ShowTooltip;
			HideTooltipEventChannel.Raised += HideTooltip;

			InputReader.TooltipActions.Position += SetTooltipTextPosition;
		}

		private void OnDisable()
		{
			ShowTooltipEventChannel.Raised -= ShowTooltip;
			HideTooltipEventChannel.Raised -= HideTooltip;

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
				_ => throw new($"{typeof(T)} is not implemented yet.")
			};
	}
}
