using BoundfoxStudios.CommunityProject.Weapons.ScriptableObjects;
using DG.Tweening;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Weapons.BallisticWeapons.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Weapons + "/Ballistic Weapon")]
	public class BallisticWeaponSO : WeaponSO
	{
		[field: SerializeField]
		[field: Range(0, 10)]
		public float MinimumRange { get; private set; }

		[field: SerializeField]
		public float RotationSpeedInDegreesPerSecond { get; private set; }

		[field: SerializeField]
		public float LaunchAnimationSpeed { get; private set; } = 0.2f;

		[field: SerializeField]
		public Ease LaunchEasing { get; private set; } = Ease.InQuart;

		[field: SerializeField]
		public float RewindAnimationSpeed { get; private set; } = 0.2f;

		[field: SerializeField]
		public Ease RewindEasing { get; private set; } = Ease.OutCirc;
	}
}
