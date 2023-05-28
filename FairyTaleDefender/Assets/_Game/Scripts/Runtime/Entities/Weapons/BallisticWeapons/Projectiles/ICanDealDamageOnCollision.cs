using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons.Projectiles
{
	public interface ICanDealDamageOnCollision
	{
		void DealDamage(Collision collision, int amount);
	}
}
