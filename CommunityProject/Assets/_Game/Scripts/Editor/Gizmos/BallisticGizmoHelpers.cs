using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BoundfoxStudios.CommunityProject.Entities.Weapons.BallisticWeapons;
using UnityEditor;
using UnityEngine;
using UnityGizmos = UnityEngine.Gizmos;

namespace BoundfoxStudios.CommunityProject.Editor.Gizmos
{
	/// <summary>
	/// As explained in <see cref="BallisticCalculationUtilities"/> all methods work on x/z plane.
	/// However for better viewing in the Editor, we draw the gizmos at the same height where the weapon is located.
	/// To see the real range of the weapon, change the editor to isometric view and view directly from top.
	/// </summary>
	public static class BallisticGizmoHelpers
	{
		private static readonly Vector3[] LineCache = new Vector3[4];
		private static readonly Dictionary<int, Vector3[]> SegmentCache = new();

		[InitializeOnLoadMethod]
		private static void ResetSegmentCache()
		{
			SegmentCache.Clear();
		}

		private static Vector3[] GetSegmentCache(int size)
		{
			if (SegmentCache.TryGetValue(size, out var result))
			{
				return result;
			}

			SegmentCache[size] = new Vector3[size];
			return SegmentCache[size];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector3
			CalculateAngledPosition(Vector3 center, Vector3 normalizedDirection, float angle, float radius) =>
			center + Quaternion.Euler(0, angle, 0) * normalizedDirection *
			radius;

		private static void DrawAttackArc(Vector3 center, Vector3 normalizedDirection, float angle, float radius,
			int segments = 20)
		{
			var step = angle / segments;
			var halfAngle = angle / 2;

			var points = GetSegmentCache(segments + 1);

			for (var i = 0; i <= segments; i++)
			{
				points[i] = CalculateAngledPosition(center, normalizedDirection, i * step - halfAngle, radius);
			}

			UnityGizmos.DrawLineStrip(points, false);
		}

		/// <summary>
		/// Draws an circle segment with an optional minimum radius.
		/// Note that the <paramref name="direction"/> corresponds to the center of the <paramref name="angle"/>.
		/// </summary>
		public static void DrawAttackSegment(
			Vector3 center,
			Vector3 direction,
			float angle,
			float radius,
			float minimumRadius = 0,
			int segments = 20)
		{
			direction = direction.normalized;

			DrawAttackArc(center, direction, angle, radius, segments);

			if (minimumRadius is not 0)
			{
				DrawAttackArc(center, direction, angle, minimumRadius, segments);
			}

			if (angle is 0 or 360)
			{
				return;
			}

			var halfAngle = angle / 2;

			LineCache[0] = CalculateAngledPosition(center, direction, -halfAngle, radius);
			LineCache[1] = CalculateAngledPosition(center, direction, -halfAngle, minimumRadius);
			LineCache[2] = CalculateAngledPosition(center, direction, halfAngle, radius);
			LineCache[3] = CalculateAngledPosition(center, direction, halfAngle, minimumRadius);

			UnityGizmos.DrawLineList(LineCache);
		}
	}
}
