using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.DirectWeapons.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Targeting + "/" + nameof(DirectTargetLocatorSO))]
	public class DirectTargetLocatorSO : TargetLocatorSO<EffectiveDirectWeaponDefinition>
	{
		public override TargetPoint? Locate(Vector3 weaponPosition, Vector3 towerForward, TargetTypeSO targetType,
			EffectiveDirectWeaponDefinition weaponDefinition)
		{
			var targets = LocateAllInRangeNonAlloc(weaponPosition, weaponDefinition.Range);
			var newSize = 0;

			for (var i = 0; i < targets; i++)
			{
				var possibleTarget = targets[i];

				if (IsInAttackRange(weaponPosition, possibleTarget.transform.position, towerForward, weaponDefinition))
				{
					// We reuse the same array here to avoid allocation.
					targets[newSize] = possibleTarget;
					newSize++;
				}
			}

			targets.Size = newSize;

			return ByTargetTypeNonAlloc(weaponPosition, targets, targetType);
		}

		// All targets we get here are in the range of the tower.
		public override bool IsInAttackRange(Vector3 weaponPosition, Vector3 targetPosition, Vector3 towerForward,
			EffectiveDirectWeaponDefinition weaponDefinition) => BallisticCalculationUtilities.IsTargetInAttackSegment(
			weaponPosition, targetPosition, towerForward, new(0, weaponDefinition.Range), weaponDefinition.AttackAngle);
	}
}
