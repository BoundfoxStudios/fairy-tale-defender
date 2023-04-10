using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.BuildSystem;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Buildable Event Channel")]
	public class BuildableEventChannelSO : EventChannelSO<BuildableEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public IAmBuildable Buildable;
		}
	}
}
