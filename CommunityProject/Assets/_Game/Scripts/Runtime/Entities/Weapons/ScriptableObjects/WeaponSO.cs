using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Entities.Weapons.ScriptableObjects
{
	public abstract class WeaponSO : ScriptableObject
	{
		[field: SerializeField]
		[field: Range(0, 10)]
		public float Range { get; private set; }

		[field: SerializeField]
		[field: Range(0, 10)]
		public float FireRateInSeconds { get; private set; }

		[field: SerializeField]
		[field: Range(0, 360)]
		public int AttackAngle { get; private set; }
	}
}
