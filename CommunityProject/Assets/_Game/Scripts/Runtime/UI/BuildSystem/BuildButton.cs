using BoundfoxStudios.CommunityProject.BuildSystem;
using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.UI.BuildSystem
{
	public abstract class BuildButton<T> : MonoBehaviour
		where T : IBuildable
	{
		[Header("References")]
		[SerializeField]
		protected T Buildable = default!;

		[Header("Broadcasting Channels")]
		[SerializeField]
		protected BuildableEventChannelSO EnterBuildModeEventChannel = default!;

		public void EnterBuildMode()
		{
			EnterBuildModeEventChannel.Raise(new() { Buildable = Buildable });
		}
	}
}
