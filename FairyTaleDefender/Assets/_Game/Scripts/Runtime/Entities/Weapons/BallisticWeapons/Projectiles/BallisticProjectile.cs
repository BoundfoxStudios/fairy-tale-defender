using BoundfoxStudios.CommunityProject.Extensions;
using BoundfoxStudios.CommunityProject.Systems.HealthSystem;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Entities.Weapons.BallisticWeapons.Projectiles
{
	[AddComponentMenu(Constants.MenuNames.Weapons + "/" + nameof(BallisticProjectile))]
	[RequireComponent(typeof(Rigidbody))]
	public class BallisticProjectile : MonoBehaviour
	{
		[field: SerializeField]
		public Collider Collider { get; private set; } = default!;

		[field: SerializeField]
		private TrailRenderer TrailRenderer { get; set; } = default!;

		private Rigidbody _rigidbody = default!;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_rigidbody.useGravity = false;

			if (TrailRenderer)
			{
				TrailRenderer.enabled = false;
			}
		}

		public void Launch(Vector3 velocity, bool doUnparent = true)
		{
			if (doUnparent)
			{
				transform.SetParent(null, true);
			}

			_rigidbody.useGravity = true;
			_rigidbody.velocity = velocity;

			if (TrailRenderer)
			{
				TrailRenderer.enabled = true;
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.collider.TryGetComponentInParent<IAmDamageable>(out var damageable))
			{
				// TODO: Use information from SO or somewhere else, no magic numbers
				damageable.Health.TakeDamage(10);
			}

			Destroy(gameObject);
		}
	}
}
