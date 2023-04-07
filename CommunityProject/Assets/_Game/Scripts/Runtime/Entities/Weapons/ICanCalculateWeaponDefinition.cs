using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Entities.Weapons
{
	public interface ICanCalculateWeaponDefinition
	{
		EffectiveWeaponDefinition CalculateEffectiveWeaponDefinition(Vector3 position);
	}
}
