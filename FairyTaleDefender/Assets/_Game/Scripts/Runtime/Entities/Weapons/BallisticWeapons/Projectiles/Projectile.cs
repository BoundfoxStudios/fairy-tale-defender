using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons.Projectiles
{
	[AddComponentMenu(Constants.MenuNames.Weapons + "/" + nameof(Projectile))]
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile : MonoBehaviour
	{
		public class LaunchArgs
		{
			public Vector3 Velocity { get; }
			public bool DoUnparent { get; set; } = true;
			public bool UseGravity { get; set; } = true;
			public int Damage { get; }

			public LaunchArgs(Vector3 velocity, int damage)
			{
				Velocity = velocity;
				Damage = damage;
			}
		}

		[field: SerializeField]
		public Collider Collider { get; private set; } = default!;

		[field: SerializeField]
		private TrailRenderer TrailRenderer { get; set; } = default!;

		private Rigidbody _rigidbody = default!;
		private int _damage;

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

		public void Launch(LaunchArgs args)
		{
			_damage = args.Damage;

			if (args.DoUnparent)
			{
				transform.SetParent(null, true);
			}

			_rigidbody.useGravity = args.UseGravity;
			_rigidbody.velocity = args.Velocity;

			if (TrailRenderer)
			{
				TrailRenderer.enabled = true;
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			DealDamage(collision, _damage);
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
