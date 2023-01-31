using BoundfoxStudios.CommunityProject.BuildSystem;
using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Infrastructure;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.UI.BuildSystem
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

		protected void OnValidate()
		{
			Guard.AgainstNull(() => Buildable, this);
			Guard.AgainstNull(() => EnterBuildModeEventChannel, this);
		}

		public void EnterBuildMode()
		{
			EnterBuildModeEventChannel.Raise(new() { Buildable = Buildable });
		}
	}
}
