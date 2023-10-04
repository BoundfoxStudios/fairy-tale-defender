using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.Listener
{
	[AddComponentMenu(Constants.MenuNames.Events + "/" + nameof(VoidEventChannelListener))]
	public class VoidEventChannelListener : MonoBehaviour
	{
		[field: SerializeField] public VoidEventChannelSO EventChannel { get; private set; } = default!;
		[field: SerializeField] public UnityEvent EventResponse { get; private set; } = default!;

		private void OnEnable()
		{
			EventChannel.Raised += Respond;
		}

		private void OnDisable()
		{
			EventChannel.Raised -= Respond;
		}

		private void Respond()
		{
			EventResponse.Invoke();
		}
	}
}
