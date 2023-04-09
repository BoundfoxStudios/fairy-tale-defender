using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Targeting + "/" + nameof(BallisticTargetLocatorSO))]
	public class BallisticTargetLocatorSO : TargetLocatorSO<EffectiveBallisticWeaponDefinition>
	{
		public override TargetPoint? Locate(Vector3 weaponPosition, Vector3 towerForward, TargetTypeSO targetType, EffectiveBallisticWeaponDefinition weaponDefinition)
		{
			var targets = LocateAllInRangeNonAlloc(weaponPosition, weaponDefinition.Range.Maximum);
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

		public override bool IsInAttackRange(Vector3 weaponPosition, Vector3 targetPosition, Vector3 towerForward,
			EffectiveBallisticWeaponDefinition weaponDefinition) =>
			BallisticCalculationUtilities.IsTargetInAttackSegment(weaponPosition, targetPosition, towerForward,
				weaponDefinition.Range, weaponDefinition.AttackAngle);
	}
}
