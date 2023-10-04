using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.Listener
{
	[AddComponentMenu(Constants.MenuNames.Events + "/" + nameof(BuildableEventChannelListener))]
	public class BuildableEventChannelListener : EventChannelListener<BuildableEventChannelSO, BuildableEventChannelSO.EventArgs>
	{

	}
}
