using UnityEngine;
using UnityEngine.Localization;

namespace BoundfoxStudios.CommunityProject.Systems.TooltipSystem
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(TextTooltip))]
	public class TextTooltip : Tooltip, ITextTooltip
	{
		[field: SerializeField]
		public LocalizedString Text { get; private set; } = default!;
	}
}
