using TMPro;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Systems.TooltipSystem
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
