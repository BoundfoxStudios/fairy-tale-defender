using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine.Splines;

namespace BoundfoxStudios.FairyTaleDefender.Extensions
{
	public static class SplineExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsValidKnotIndex(this ISpline spline, int knotIndex) => knotIndex >= 0 && knotIndex < spline.Count;
	}
}
