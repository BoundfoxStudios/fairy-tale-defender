using UnityEngine;
using UnityEngine.Events;

namespace BoundfoxStudios.CommunityProject.Events.ScriptableObjects
{
	/// <summary>
	/// Base EventChannel without an argument.
	/// </summary>
	public abstract class EventChannelSO : ScriptableObject
	{
		public UnityAction Raised = delegate { };

		private void OnDisable()
		{
			Raised -= Log;
		}

		private void OnEnable()
		{
			Raised += Log;
		}

		public void Raise()
		{
			Raised.Invoke();
		}

		private void Log()
		{
			Debug.Log($"<b><color=yellow>Event</color></b> {name} raised!");
		}
	}

	/// <summary>
	/// Base EventChannel with an argument.
	/// </summary>
	public abstract class EventChannelSO<T> : ScriptableObject
	{
		public UnityAction<T> Raised = delegate { };

		private void OnDisable()
		{
			Raised -= Log;
		}

		private void OnEnable()
		{
			Raised += Log;
		}

		public void Raise(T value)
		{
			Raised.Invoke(value);
		}

		private void Log(T value)
		{
			Debug.Log($"<b><color=yellow>Event</color></b> {name} raised with type {value.GetType()} and value {value}!");
		}
	}
}
