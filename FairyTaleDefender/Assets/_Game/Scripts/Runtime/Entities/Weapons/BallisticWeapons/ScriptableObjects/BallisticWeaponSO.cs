using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure;
using DG.Tweening;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Weapons + "/Ballistic Weapon")]
	public class BallisticWeaponSO : WeaponSO
	{
		[field: SerializeField]
		[field: Range(0, 10)]
		public float MinimumRange { get; private set; }

		public float MaximumRange => base.Range;
		public new Limits2 Range => new(MinimumRange, MaximumRange);

		[field: SerializeField]
		[field: Range(0, 720)]
		public float RotationSpeedInDegreesPerSecond { get; private set; }

		[field: SerializeField]
		[field: Range(0, 5)]
		public float LaunchAnimationTimeInSeconds { get; private set; } = 0.2f;

		[field: SerializeField]
		public Ease LaunchEasing { get; private set; } = Ease.InQuart;

		[field: SerializeField]
		[field: Range(0, 5)]
		public float RewindAnimationTimeInSeconds { get; private set; } = 0.2f;

		[field: SerializeField]
		public Ease RewindEasing { get; private set; } = Ease.OutCirc;
	}
}
