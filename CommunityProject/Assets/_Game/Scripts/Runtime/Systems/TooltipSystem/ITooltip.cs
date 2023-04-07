using UnityEngine.Localization;

namespace BoundfoxStudios.CommunityProject.Systems.TooltipSystem
{
	public interface ITooltip
	{
		// Marker.
	}

	public interface ITextTooltip : ITooltip
	{
		LocalizedString Text { get; }
	}
}
