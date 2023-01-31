using BoundfoxStudios.CommunityProject.Weapons.Targeting;
using BoundfoxStudios.CommunityProject.Weapons.Targeting.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Weapons.BallisticWeapons.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Targeting + "/" + nameof(BallisticTargetLocator))]
	public class BallisticTargetLocator : TargetLocator<BallisticWeaponSO>
	{
		[field: SerializeField]
		[field: Tooltip("Specifies how much further a target can be reached depending on the height of the weapon.")]
		public float HeightToRangeFactor { get; private set; } = 1.01f;

		public override TargetPoint? Locate(Vector3 weaponPosition, Vector3 towerForward, TargetType targetType, BallisticWeaponSO weaponDefinition)
		{
			// TODO: Calculate in the HeightToRangeFactor, for this we also need the y-position of the weapon minus the tower's height.
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
			BallisticWeaponSO weaponDefinition) =>
			BallisticCalculationUtilities.IsTargetInAttackSegment(weaponPosition, targetPosition, towerForward,
				weaponDefinition.Range, weaponDefinition.AttackAngle);
	}
}
