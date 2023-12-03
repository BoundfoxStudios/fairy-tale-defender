using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.DirectWeapons.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Weapons + "/Direct Weapon")]
	public class DirectWeaponSO : WeaponSO
	{
		[field: SerializeField]
		[field: Range(0, 720)]
		public float RotationSpeedInDegreesPerSecond { get; private set; }

		[field: SerializeField]
		[field: Range(0, 5)]
		public float RewindAnimationTimeInSeconds { get; private set; } = 0.2f;
	}
}
