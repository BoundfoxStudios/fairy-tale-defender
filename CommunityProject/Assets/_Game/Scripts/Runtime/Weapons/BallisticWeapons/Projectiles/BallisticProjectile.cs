using BoundfoxStudios.CommunityProject.Extensions;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Weapons.BallisticWeapons.Projectiles
{
	[AddComponentMenu(Constants.MenuNames.Weapons + "/" + nameof(BallisticProjectile))]
	[RequireComponent(typeof(Rigidbody))]
	public class BallisticProjectile : MonoBehaviour
	{
		[field: SerializeField]
		public Collider Collider { get; private set; } = default!;

		[SerializeField]
		private TrailRenderer TrailRenderer = default!;

		private Rigidbody _rigidbody = default!;

		private void Awake()
		{
			_rigidbody = gameObject.GetComponentSafe<Rigidbody>();
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
	}
}
