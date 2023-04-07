using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Entities.Weapons
{
	public interface ICanCalculateEffectiveWeaponDefinition
	{
		EffectiveWeaponDefinition CalculateEffectiveWeaponDefinition(Vector3 position);
	}
}
