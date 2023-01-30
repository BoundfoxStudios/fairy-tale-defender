using System.Runtime.CompilerServices;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Extensions
{
	public static class GameObjectExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Deactivate(this GameObject gameObject)
		{
			if (gameObject.activeSelf)
			{
				gameObject.SetActive(false);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Activate(this GameObject gameObject)
		{
			if (!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T GetComponentSafe<T>(this GameObject gameObject)
		{
			var component = gameObject.GetComponent<T>();
			Debug.Assert(component is not null, $"Did not find {typeof(T).Name}", gameObject);
			return component;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T GetComponentInParentSafe<T>(this GameObject gameObject, bool includeInactive = false)
		{
			var component = gameObject.GetComponentInParent<T>(includeInactive);
			Debug.Assert(component is not null, $"Did not find {typeof(T).Name}", gameObject);
			return component;
		}
	}
}
