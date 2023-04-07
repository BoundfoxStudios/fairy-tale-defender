using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Extensions
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
		public static bool IsInLayerMask(this GameObject gameObject, int layerMask) =>
			layerMask == (layerMask | (1 << gameObject.layer));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetComponentInParent<T>(this Component gameObject, [NotNullWhen(true)] out T? component)
		{
			component = gameObject.GetComponentInParent<T>();
			return component is not null;
		}
	}
}
