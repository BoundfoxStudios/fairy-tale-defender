using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Entities.Weapons.ScriptableObjects
{
	public abstract class EffectiveWeaponCalculatorSO<TWeaponSO, TEffectiveWeaponDefinition> : ScriptableObject
		where TWeaponSO : WeaponSO
		where TEffectiveWeaponDefinition : EffectiveWeaponDefinition
	{
		public abstract TEffectiveWeaponDefinition Calculate(TWeaponSO weaponDefinition, Vector3 towerPosition);
	}
}
