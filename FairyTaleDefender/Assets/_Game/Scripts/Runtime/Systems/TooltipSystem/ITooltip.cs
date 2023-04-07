using UnityEngine.Localization;

namespace BoundfoxStudios.FairyTaleDefender.Systems.TooltipSystem
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
