using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons
{
	public interface ICanCalculateEffectiveWeaponDefinition
	{
		EffectiveWeaponDefinition CalculateEffectiveWeaponDefinition(Vector3 position);
	}
}
