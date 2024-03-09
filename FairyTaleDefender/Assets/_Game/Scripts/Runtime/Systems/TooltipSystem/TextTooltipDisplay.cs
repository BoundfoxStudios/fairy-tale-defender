using BoundfoxStudios.FairyTaleDefender.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace BoundfoxStudios.FairyTaleDefender.Systems.TooltipSystem
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(TextTooltipDisplay))]
	public class TextTooltipDisplay : TooltipDisplay
	{
		[field: SerializeField]
		private LocalizeStringEvent TextLocalizeString { get; set; } = default!;

		protected override void SetTooltip<T>(T tooltip)
		{
			var resolvedTooltip = ResolveTooltip<ITextTooltip, T>(tooltip);
			TextLocalizeString.StringReference = resolvedTooltip.Text;
		}
	}
}
