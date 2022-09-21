using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace BoundfoxStudios.CommunityProject.Events.ScriptableObjects
{
	public abstract class EventChannelBaseSO : ScriptableObject
	{
#if UNITY_EDITOR
		/// <summary>
		/// In-Editor description to let developers for what certain event instances are.
		/// </summary>
		[TextArea]
		[SerializeField]
		[UsedImplicitly]
		private string Description;
#endif
	}

	/// <summary>
	/// Base EventChannel without an argument.
	/// </summary>
	public abstract class EventChannelSO : EventChannelBaseSO
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
	public abstract class EventChannelSO<T> : EventChannelBaseSO
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
			Debug.Log($"<b><color=yellow>Event</color></b> {name} raised with type {value.GetType().Name} and value {value}!");
		}
	}
}
