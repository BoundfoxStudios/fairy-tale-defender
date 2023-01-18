using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Weapons.Bullets
{
	[AddComponentMenu(Constants.MenuNames.Weapons + "/" + nameof(BallisticProjectile))]
	[RequireComponent(typeof(Rigidbody))]
	public class BallisticProjectile : MonoBehaviour
	{
		[field: SerializeField]
		public Collider Collider { get; private set; }

		private Rigidbody _rigidbody;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_rigidbody.useGravity = false;
		}

		public void Launch(Vector3 velocity, bool doUnparent = true)
		{
			if (doUnparent)
			{
				transform.SetParent(null, true);
			}

			_rigidbody.useGravity = true;
			_rigidbody.velocity = velocity;
		}
	}
}
