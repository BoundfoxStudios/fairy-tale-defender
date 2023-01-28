using System.Runtime.CompilerServices;
using BoundfoxStudios.CommunityProject.Weapons.BallisticWeapons;
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
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector3
			CalculateAngledPosition(Vector3 center, Vector3 normalizedDirection, float angle, float radius) =>
			center + Quaternion.Euler(0, angle, 0) * normalizedDirection *
			radius;

		private static void DrawAttackArc(Vector3 center, Vector3 normalizedDirection, float angle, float radius,
			float segments = 20)
		{
			var step = angle / segments;
			var halfAngle = angle / 2;

			var lastPosition = CalculateAngledPosition(center, normalizedDirection, -halfAngle, radius);

			for (var i = 0; i <= segments; i++)
			{
				var position = CalculateAngledPosition(center, normalizedDirection, i * step - halfAngle, radius);

				UnityGizmos.DrawLine(lastPosition, position);

				lastPosition = position;
			}
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
			float resolution = 20)
		{
			direction = direction.normalized;

			DrawAttackArc(center, direction, angle, radius, resolution);

			if (minimumRadius is not 0)
			{
				DrawAttackArc(center, direction, angle, minimumRadius, resolution);
			}

			if (angle is 0 or 360)
			{
				return;
			}

			var halfAngle = angle / 2;
			UnityGizmos.DrawLine(
				CalculateAngledPosition(center, direction, -halfAngle, radius),
				CalculateAngledPosition(center, direction, -halfAngle, minimumRadius)
			);
			UnityGizmos.DrawLine(
				CalculateAngledPosition(center, direction, halfAngle, radius),
				CalculateAngledPosition(center, direction, halfAngle, minimumRadius)
			);
		}
	}
}
