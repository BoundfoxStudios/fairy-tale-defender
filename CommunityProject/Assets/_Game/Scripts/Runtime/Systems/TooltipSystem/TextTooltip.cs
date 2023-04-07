using UnityEngine;
using UnityEngine.Localization;

namespace BoundfoxStudios.CommunityProject.Systems.TooltipSystem
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(TextTooltip))]
	public class TextTooltip : Tooltip2, ITextTooltip
	{
		[field: SerializeField]
		public LocalizedString Text { get; private set; } = default!;

		protected override ITooltip GetTooltip() => this;
	}
}
