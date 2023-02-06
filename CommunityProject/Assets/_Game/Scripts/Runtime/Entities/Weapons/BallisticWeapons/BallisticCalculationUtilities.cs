using BoundfoxStudios.CommunityProject.Extensions;
using BoundfoxStudios.CommunityProject.Infrastructure;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Entities.Weapons.BallisticWeapons
{
	/// <summary>
	/// This class contains helper methods to calculate weapon ballistics.
	/// </summary>
	public static class BallisticCalculationUtilities
	{
		/// <summary>
		/// Note: This method only works on a 2D plane to make targeting easier.
		/// It does not take height into account.
		/// Adjust the <paramref name="range"/> before if you want to take height into account.
		/// </summary>
		public static bool IsTargetInAttackSegment(
			Vector3 weaponPosition,
			Vector3 targetPosition,
			Vector3 towerForward,
			Limits2 range,
			float attackAngle)
		{
			var weaponPositionVector2 = weaponPosition.ToXZ();
			var targetPositionVector2 = targetPosition.ToXZ();
			var towerForwardVector2 = towerForward.ToXZ();
			var rangeSquared = Mathf.Pow(range.Maximum, 2);
			var minimumRangeSquared = Mathf.Pow(range.Minimum, 2);

			var distanceSquared = weaponPositionVector2.DistanceSquaredTo(targetPositionVector2);

			if (distanceSquared < minimumRangeSquared || distanceSquared > rangeSquared)
			{
				return false;
			}

			var halfAngle = attackAngle / 2;
			var targetAngle = Vector2.SignedAngle(towerForwardVector2, targetPositionVector2 - weaponPositionVector2);

			if (targetAngle < -halfAngle || targetAngle > halfAngle)
			{
				return false;
			}

			return true;
		}

		private static void CalculateBallisticArcVelocityY(Vector3 launchPoint, Vector3 target, Vector3 gravity, out Vector3 velocityY,
			out float time)
		{
			var maximumHeight = launchPoint.y < target.y ? target.y + 1 : launchPoint.y + 1;
			var displacementY = target.y - launchPoint.y;

			time = Mathf.Sqrt(-2 * maximumHeight / gravity.y) + Mathf.Sqrt(2 * (displacementY - maximumHeight) / gravity.y);
			velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity.y * maximumHeight);
		}

		/// <summary>
		/// Calculates the velocity needed to reach the <paramref name="target"/> from the given <paramref name="launchPoint"/>.
		/// If a <paramref name="weaponForward"/> is given, the method will adjust the velocity in direction of weaponForward.
		/// That makes sense for ballistic weapon that may be slow to rotate to their current target.
		/// That could mean that the weapon will not hit the target because it's not yet rotated to the target.
		/// </summary>
		// TODO: It would be best the include the launchAngle here and calculate the velocity under a given angle.
		// ^ implementing this could mean that a ballistic weapon will not shoot, even if the target is in range,
		// because it not possible to have a velocity under a given angle that is able to hit the target.
		// This would also invalidate how the range is calculated, because we'd need a minimum and maximum launch angle,
		// they specify the actual range. That may be a bit too much for a little tower defense. :)
		// TODO: We may need to rethink ballistics to hit targets that are way above are way below the launchPoint.
		// ^ this should be resolved even if it leads to some visual interesting shooting, but it will eventually hit the target.
		// Otherwise we could leave it and have some inaccuracy added to the game.
		public static Vector3 CalculateBallisticArcVelocity(Vector3 launchPoint, Vector3 target, Vector3? weaponForward)
		{
			CalculateBallisticArcVelocityY(launchPoint, target, Physics.gravity, out var velocityY, out var time);

			if (weaponForward is not null)
			{
				var distance = Vector3.Distance(target, launchPoint);
				target = launchPoint + weaponForward.Value.normalized * distance;
			}

			var displacementXZ = new Vector3(target.x - launchPoint.x, 0, target.z - launchPoint.z);
			var velocityXZ = displacementXZ / time;

			return velocityXZ + velocityY;
		}
	}
}
