using System.Runtime.CompilerServices;
using BoundfoxStudios.CommunityProject.Infrastructure;
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
			Guard.AgainstNull(() => component, gameObject);
			return component;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T GetComponentInParentSafe<T>(this GameObject gameObject, bool includeInactive = false)
		{
			var component = gameObject.GetComponentInParent<T>(includeInactive);
			Guard.AgainstNull(() => component, gameObject);
			return component;
		}
	}
}
