using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.TooltipSystem;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(fileName = Constants.FileNames.EventChannelSuffix,
		menuName = Constants.MenuNames.Events + "/Tooltip" + Constants.MenuNames.EventChannelSuffix)]
	public class TooltipEventChannelSO : EventChannelSO<TooltipEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public ITooltip Tooltip;
			public Vector2 ScreenPosition;
		}
	}
}
