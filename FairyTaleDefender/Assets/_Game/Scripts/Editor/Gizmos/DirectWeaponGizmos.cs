using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.DirectWeapons;
using UnityEditor;
using UnityEngine;
using UnityGizmos = UnityEngine.Gizmos;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Gizmos
{
	public static class DirectWeaponGizmos
	{
		[DrawGizmo(GizmoType.Active | GizmoType.Selected | GizmoType.InSelectionHierarchy,
			typeof(DirectWeaponDiagnostics))]
		private static void DrawBallisticWeaponGizmos(DirectWeaponDiagnostics directWeaponDiagnostics,
			GizmoType gizmoType)
		{
			var directWeapon = directWeaponDiagnostics.Weapon;

			DrawAttackAngle(directWeapon);
		}

		private static void DrawAttackAngle(DirectWeapon directWeapon)
		{
			var weaponTransform = directWeapon.transform;

			// If we're viewing the weapon prefab, we may not have a tower
			var forward = directWeapon.Tower ? directWeapon.Tower.transform.forward : weaponTransform.forward;
			var effectiveWeaponDefinition =
				(EffectiveDirectWeaponDefinition)directWeapon.CalculateEffectiveWeaponDefinition(directWeapon.Tower
					? directWeapon.Tower.transform.position
					: weaponTransform.position);
			UnityGizmos.color = new(1, 0.976f, 0.102f, 0.5f);
			BallisticGizmoHelpers.DrawAttackSegment(
				weaponTransform.position,
				forward,
				effectiveWeaponDefinition.AttackAngle,
				0,
				effectiveWeaponDefinition.Range
			);
		}
	}
}
