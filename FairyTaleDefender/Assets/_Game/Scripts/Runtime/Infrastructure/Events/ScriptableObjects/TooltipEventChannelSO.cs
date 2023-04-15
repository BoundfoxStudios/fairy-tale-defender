using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.TooltipSystem;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Tooltip Event Channel")]
	public class TooltipEventChannelSO : EventChannelSO<TooltipEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public ITooltip Tooltip;
			public Vector2 ScreenPosition;
		}
	}
}
