using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons.Projectiles;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Systems.HealthSystem;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons
{
	[AddComponentMenu(Constants.MenuNames.Weapons + "/" + nameof(AreaDamageOnCollision))]
	public class AreaDamageOnCollision : MonoBehaviour, ICanDealDamageOnCollision
	{
		[field: SerializeField]
		private float Radius { get; set; }

		[field: SerializeField]
		private LayerMask EnemyLayer { get; set; }

		[field: SerializeField]
		private GameObject? Effect { get; set; }

		public void DealDamage(Collision collision, int amount)
		{
			var position = collision.transform.position;
			TrySpawnEffect(position);

			var results = Physics.SphereCastAll(position, Radius,
				Vector3.up, Radius, EnemyLayer);

			foreach (var hit in results)
			{
				if (hit.collider.TryGetComponentInParent<IAmDamageable>(out var damageable))
				{
					damageable.Health.TakeDamage(amount);
				}
			}
		}

		private void TrySpawnEffect(Vector3 position)
		{
			if (!Effect)
			{
				return;
			}

			Instantiate(Effect, position, Quaternion.identity);
		}
	}
}
