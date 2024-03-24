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
		private bool _hasDealtDamage;

		public void DealDamage(Collision collision, int amount)
		{
			if (!_hasDealtDamage && collision.collider.TryGetComponentInParent<IAmDamageable>(out var damageable))
			{
				_hasDealtDamage = true;
				damageable.Health.TakeDamage(amount);
			}
		}
	}
}
