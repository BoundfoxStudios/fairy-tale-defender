using BoundfoxStudios.CommunityProject.Systems.TooltipSystem;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Tooltip Event Channel")]
	public class TooltipEventChannelSO : EventChannelSO<TooltipEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public ITooltip2 Tooltip;
			public Vector2 ScreenPosition;
		}
	}
}
