using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using BoundfoxStudios.CommunityProject.Extensions;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Infrastructure.RuntimeAnchors.ScriptableObjects
{
	/// <summary>
	/// A runtime anchor is an Unity managed singleton in form of a ScriptableObject.
	/// Instead of implementing a Singleton in a MonoBehaviour, we use a runtime anchor.
	/// This allows to connect singletons via the Unity inspector.
	/// </summary>
	public abstract class RuntimeAnchorBaseSO<T> : ScriptableObject
		where T : class
	{
		public bool IsSet { get; private set; }
		private T? _item;

		public T? Item
		{
			get => _item;
			set
			{
				_item = value;
				IsSet = _item is not null;
			}
		}

		public T ItemSafe => Item.EnsureOrThrow();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryGetItem([NotNullWhen(true)] out T? item)
		{
			item = _item;
			return _item is not null;
		}

		private void OnDisable()
		{
			_item = null;
			IsSet = false;
		}
	}
}
