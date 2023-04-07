using BoundfoxStudios.CommunityProject.Systems.BuildSystem;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects
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
