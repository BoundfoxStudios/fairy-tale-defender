using BoundfoxStudios.CommunityProject.BuildSystem;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Events.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Buildable Event Channel")]
	public class BuildableEventChannelSO : EventChannelSO<BuildableEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public IBuildable Buildable;
		}
	}
}
