using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons.Projectiles;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Systems.HealthSystem;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons
{
	public class AreaDamageOnCollision : MonoBehaviour, ICanDealDamageOnCollision
	{
		[field: SerializeField]
		private float Radius { get; set; }

		[field: SerializeField]
		private LayerMask EnemyLayer { get; set; }

		public void DealDamage(Collision collision, int amount)
		{
			var results = Physics.SphereCastAll(collision.transform.position, Radius,
				Vector3.up, Radius, EnemyLayer);

			foreach (var hit in results)
			{
				if (hit.collider.TryGetComponentInParent<IAmDamageable>(out var damageable))
				{
					damageable.Health.TakeDamage(amount);
				}
			}
		}
	}
}
