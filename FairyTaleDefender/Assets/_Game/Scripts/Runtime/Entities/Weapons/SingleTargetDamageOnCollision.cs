using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons.Projectiles;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Systems.HealthSystem;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons
{
	[AddComponentMenu(Constants.MenuNames.Weapons + "/" + nameof(SingleTargetDamageOnCollision))]
	public class SingleTargetDamageOnCollision : MonoBehaviour, ICanDealDamageOnCollision
	{
		public void DealDamage(Collision collision, int amount)
		{
			if (collision.collider.TryGetComponentInParent<IAmDamageable>(out var damageable))
			{
				damageable.Health.TakeDamage(amount);
			}
		}
	}
}
