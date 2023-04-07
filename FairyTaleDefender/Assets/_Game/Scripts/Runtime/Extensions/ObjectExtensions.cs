using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BoundfoxStudios.FairyTaleDefender.Extensions
{
	public static class ObjectExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NotNull]
		public static T EnsureOrThrow<T>(this T? obj)
		{
			if (obj is null)
			{
				throw new NullReferenceException($"{nameof(obj)} is null");
			}

			return obj;
		}

		/// <summary>
		/// Checks if a <see cref="UnityEngine.Object"/> is not null and that the native object is alive.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Exists([NotNullWhen(true)] this UnityEngine.Object? unityObject) => unityObject is not null && unityObject;
	}
}
