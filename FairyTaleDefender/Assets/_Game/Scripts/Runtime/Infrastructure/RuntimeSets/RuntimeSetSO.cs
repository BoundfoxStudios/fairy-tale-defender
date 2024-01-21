using System.Collections.Generic;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeSets
{
	public abstract class RuntimeSetSO<T> : ScriptableObject
	{
		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		public VoidEventChannelSO? RuntimeSetChangedEventChannel { get; private set; }

		public List<T> Items { get; } = new();

		private void OnEnable()
		{
			Clear();
		}

		private void OnDisable()
		{
			Clear();
		}

		public void Add(T objectToAdd)
		{
			if (Items.Contains(objectToAdd))
			{
				return;
			}

			Items.Add(objectToAdd);

			if (RuntimeSetChangedEventChannel.Exists())
			{
				RuntimeSetChangedEventChannel.Raise();
			}
		}

		public bool Remove(T objectToRemove)
		{
			if (!Items.Contains(objectToRemove))
			{
				return false;
			}

			Items.Remove(objectToRemove);

			if (RuntimeSetChangedEventChannel.Exists())
			{
				RuntimeSetChangedEventChannel.Raise();
			}

			return true;
		}

		public void Clear()
		{
			Items.Clear();

			if (RuntimeSetChangedEventChannel.Exists())
			{
				RuntimeSetChangedEventChannel.Raise();
			}
		}
	}
}
