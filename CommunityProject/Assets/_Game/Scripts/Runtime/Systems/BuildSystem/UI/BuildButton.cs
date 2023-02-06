using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Systems.BuildSystem.UI
{
	public abstract class BuildButton<T> : MonoBehaviour
		where T : IBuildable
	{
		[field: Header("References")]
		[field: SerializeField]
		protected T Buildable { get; set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		protected BuildableEventChannelSO EnterBuildModeEventChannel { get; set; } = default!;

		public void EnterBuildMode()
		{
			EnterBuildModeEventChannel.Raise(new() { Buildable = Buildable });
		}
	}
}
