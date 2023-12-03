using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.DirectWeapons.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Weapons + "/" + nameof(EffectiveDirectWeaponCalculatorSO))]
	public class EffectiveDirectWeaponCalculatorSO : EffectiveWeaponCalculatorSO<DirectWeaponSO, EffectiveDirectWeaponDefinition>
	{
		public override EffectiveDirectWeaponDefinition Calculate(DirectWeaponSO weaponDefinition, Vector3 towerPosition)
		{
			return new(weaponDefinition.Range, weaponDefinition.AttackAngle, weaponDefinition.FireRateEverySeconds);
		}
	}
}
