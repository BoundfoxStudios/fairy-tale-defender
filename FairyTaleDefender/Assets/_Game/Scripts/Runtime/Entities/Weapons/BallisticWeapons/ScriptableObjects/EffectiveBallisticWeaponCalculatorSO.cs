using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Weapons + "/" + nameof(EffectiveBallisticWeaponCalculatorSO))]
	public class EffectiveBallisticWeaponCalculatorSO : EffectiveWeaponCalculatorSO<BallisticWeaponSO, EffectiveBallisticWeaponDefinition>
	{
		[field: SerializeField]
		[field: Tooltip("Specifies how much further a target can be reached depending on the height of the weapon.")]
		public float HeightToRangeFactor { get; private set; } = 1.01f;

		public override EffectiveBallisticWeaponDefinition Calculate(BallisticWeaponSO weaponDefinition, Vector3 towerPosition)
		{
			var range = CalculateMaximumRange(weaponDefinition, towerPosition);
			var result = new EffectiveBallisticWeaponDefinition(range, weaponDefinition.FireRateEverySeconds, weaponDefinition.AttackAngle);
			return result;
		}

		private Limits2 CalculateMaximumRange(BallisticWeaponSO weaponDefinition, Vector3 towerPosition)
		{
			var range = new Limits2(weaponDefinition.MinimumRange,
				weaponDefinition.MaximumRange + Mathf.Max(0, towerPosition.y) * HeightToRangeFactor);
			return range;
		}
	}
}
