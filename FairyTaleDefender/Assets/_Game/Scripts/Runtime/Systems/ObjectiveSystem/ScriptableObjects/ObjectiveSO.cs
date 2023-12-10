using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.ObjectiveSystem.ScriptableObjects
{
	public abstract class ObjectiveSO : ScriptableObject
	{
		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		public VoidEventChannelSO ObjectiveCompletedEventChannel { get; private set; } = default!;

		public bool IsCompleted { get; private set; }

		protected void Complete()
		{
			IsCompleted = true;
			ObjectiveCompletedEventChannel.Raise();
		}
	}
}
