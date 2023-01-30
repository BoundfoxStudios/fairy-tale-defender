using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BoundfoxStudios.CommunityProject.Extensions
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
	}
}
