using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Weapons.ScriptableObjects
{
	public abstract class WeaponSO : ScriptableObject
	{
		[field: SerializeField]
		public float Range { get; private set; }

		[field: SerializeField]
		public float FireRateInSeconds { get; private set; }

		[field: SerializeField]
		[field: Range(0, 360)]
		public float AttackAngle { get; private set; }
	}
}
