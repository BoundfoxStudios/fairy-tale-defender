using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.BuildSystem;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(fileName = Constants.FileNames.EventChannelSuffix,
		menuName = Constants.MenuNames.Events + "/Buildable" + Constants.MenuNames.EventChannelSuffix)]
	public class BuildableEventChannelSO : EventChannelSO<BuildableEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public IAmBuildable Buildable;
		}
	}
}
