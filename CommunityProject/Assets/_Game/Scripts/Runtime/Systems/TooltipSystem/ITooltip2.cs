using UnityEngine.Localization;

namespace BoundfoxStudios.CommunityProject.Systems.TooltipSystem
{
	public interface ITooltip2
	{
		// Marker.
	}

	public interface ITextTooltip : ITooltip2
	{
		LocalizedString Text { get; }
	}
}
