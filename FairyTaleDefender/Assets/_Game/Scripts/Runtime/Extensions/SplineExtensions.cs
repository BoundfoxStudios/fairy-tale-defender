using System.Runtime.CompilerServices;
using UnityEngine.Splines;

namespace BoundfoxStudios.CommunityProject.Extensions
{
	public static class SplineExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsValidKnotIndex(this ISpline spline, int knotIndex) => knotIndex >= 0 && knotIndex < spline.Count;
	}
}
