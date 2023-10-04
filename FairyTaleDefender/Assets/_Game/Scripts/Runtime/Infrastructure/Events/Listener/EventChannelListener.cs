using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.Listener
{
	public class EventChannelListener<TEventChannelSO, T> : MonoBehaviour where TEventChannelSO : EventChannelSO<T>
	{
		[field: SerializeField] public TEventChannelSO EventChannel { get; private set; } = default!;
		[field: SerializeField] public UnityEvent<T> EventResponse { get; private set; } = default!;

		private void OnEnable()
		{
			EventChannel.Raised += Respond;
		}

		private void OnDisable()
		{
			EventChannel.Raised -= Respond;
		}

		private void Respond(T args)
		{
			EventResponse.Invoke(args);
		}
	}
}
