using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.ScriptableObjects
{
	public abstract class WeaponSO : ScriptableObject
	{
		[field: SerializeField]
		[field: Range(0, 10)]
		public float Range { get; private set; }

		[field: SerializeField]
		[field: Range(0, 10)]
		public float FireRateEverySeconds { get; private set; }

		[field: SerializeField]
		[field: Range(0, 360)]
		public int AttackAngle { get; private set; }

		[field: SerializeField]
		[field: Min(0)]
		public int Damage { get; private set; } = 10;
	}
}
