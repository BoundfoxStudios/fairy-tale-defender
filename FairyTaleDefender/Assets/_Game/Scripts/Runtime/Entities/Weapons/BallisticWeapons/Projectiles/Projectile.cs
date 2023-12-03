using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons.Projectiles
{
	[AddComponentMenu(Constants.MenuNames.Weapons + "/" + nameof(Projectile))]
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile : MonoBehaviour
	{
		[field: SerializeField]
		public Collider Collider { get; private set; } = default!;

		[field: SerializeField]
		private TrailRenderer TrailRenderer { get; set; } = default!;

		private Rigidbody _rigidbody = default!;

		private ICanDealDamageOnCollision DamageOnCollision { get; set; } = default!;

		private void Awake()
		{
			DamageOnCollision = GetComponent<ICanDealDamageOnCollision>();
			_rigidbody = GetComponent<Rigidbody>();
			_rigidbody.useGravity = false;

			if (TrailRenderer)
			{
				TrailRenderer.enabled = false;
			}
		}

		public void Launch(Vector3 velocity,
			bool doUnparent = true,
			bool useGravity = true
		)
		{
			if (doUnparent)
			{
				transform.SetParent(null, true);
			}

			_rigidbody.useGravity = useGravity;
			_rigidbody.velocity = velocity;

			if (TrailRenderer)
			{
				TrailRenderer.enabled = true;
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			// TODO: Use information from SO or somewhere else, no magic numbers
			DealDamage(collision, 10);
			Destroy(gameObject);
		}

		private void DealDamage(Collision collision, int amount)
		{
			DamageOnCollision.DealDamage(collision, amount);
		}

		private void OnValidate()
		{
			Debug.Assert(GetComponent<ICanDealDamageOnCollision>() != null,
				$"No component is implementing {nameof(ICanDealDamageOnCollision)} on {this}");
		}
	}
}
