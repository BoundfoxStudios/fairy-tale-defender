using BoundfoxStudios.FairyTaleDefender.Common;
using TMPro;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.TooltipSystem
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(TextTooltipDisplay))]
	public class TextTooltipDisplay : TooltipDisplay
	{
		[field: SerializeField]
		private TextMeshProUGUI Text { get; set; } = default!;

		protected override void SetTooltip<T>(T tooltip)
		{
			var resolvedTooltip = ResolveTooltip<ITextTooltip, T>(tooltip);
			Text.text = resolvedTooltip.Text.GetLocalizedString();
		}
	}
}
