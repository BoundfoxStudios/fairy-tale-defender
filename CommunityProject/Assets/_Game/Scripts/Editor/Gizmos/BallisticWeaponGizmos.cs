using BoundfoxStudios.CommunityProject.Weapons.BallisticWeapons;
using UnityEditor;
using UnityEngine;
using UnityGizmos = UnityEngine.Gizmos;

namespace BoundfoxStudios.CommunityProject.Editor.Gizmos
{
	public static class BallisticWeaponGizmos
	{
		[DrawGizmo(GizmoType.Active | GizmoType.Selected | GizmoType.InSelectionHierarchy, typeof(BallisticWeaponDiagnostics))]
		private static void DrawBallisticWeaponGizmos(BallisticWeaponDiagnostics ballisticWeaponDiagnostics, GizmoType gizmoType)
		{
			var ballisticWeapon = ballisticWeaponDiagnostics.Weapon;

			DrawArmRotationAngle(ballisticWeapon);
			DrawAttackAngle(ballisticWeapon);
		}

		private static void DrawAttackAngle(BallisticWeapon ballisticWeapon)
		{
			var weaponTransform = ballisticWeapon.transform;

			// If we're viewing the weapon prefab, we may not have a tower
			var forward = ballisticWeapon.Tower ? ballisticWeapon.Tower.transform.forward : weaponTransform.forward;

			UnityGizmos.color = new(1, 0.976f, 0.102f, 0.5f);
			BallisticGizmoHelpers.DrawAttackSegment(
				weaponTransform.position,
				forward,
				ballisticWeapon.WeaponDefinition.AttackAngle,
				ballisticWeapon.WeaponDefinition.Range,
				ballisticWeapon.WeaponDefinition.MinimumRange
			);
		}

		private static void DrawArmRotationAngle(BallisticWeapon ballisticWeapon)
		{
			var arm = ballisticWeapon.Arm;
			var weaponTransform = ballisticWeapon.transform;
			var launchPointTransform = arm.LaunchPoint;
			var armTransform = arm.ArmPivot;
			var armPosition = armTransform.position;
			var distance = (armPosition - launchPointTransform.position).magnitude;
			var startVector = Quaternion.AngleAxis(arm.XRotation.x, weaponTransform.right) * weaponTransform.forward;

			Handles.color = new(1, 1, 1, 0.3f);
			Handles.DrawSolidArc(armPosition, armTransform.right, startVector, arm.XRotation.y - arm.XRotation.x, -distance);
		}
	}
}
