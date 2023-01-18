using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Weapons.ScriptableObjects
{
	public abstract class WeaponSO : ScriptableObject
	{
		public float Range;
		public float FireRateInSeconds;

		[Range(0, 360)]
		public float AttackAngle;
	}
}
