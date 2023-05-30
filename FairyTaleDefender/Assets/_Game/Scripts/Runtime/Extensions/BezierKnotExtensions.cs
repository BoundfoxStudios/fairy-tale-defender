using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine.Splines;

namespace BoundfoxStudios.FairyTaleDefender.Extensions
{
	public static class BezierKnotExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsCloseTo(this BezierKnot knot, BezierKnot otherKnot, float epsilon = 0.001f) =>
			math.distancesq(knot.Position, otherKnot.Position) <= epsilon;
	}
}
