using System;
using System.Threading;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons.Projectiles;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons
{
	/// <summary>
	/// Class for all ballistic weapons, like a catapult.
	/// </summary>
	[AddComponentMenu(Constants.MenuNames.Weapons + "/" + nameof(BallisticWeapon))]
	public class BallisticWeapon : Weapon<BallisticWeaponSO, BallisticTargetLocatorSO, EffectiveBallisticWeaponDefinition>
	{
		[field: SerializeField]
		public ArmSettings Arm { get; private set; } = default!;

		[field: SerializeField]
		private BallisticProjectile ProjectilePrefab { get; set; } = default!;

		private BallisticProjectile? _projectile;
		private Quaternion _targetRotation;

		[Serializable]
		public class ArmSettings
		{
			[field: SerializeField]
			public Transform ArmPivot { get; private set; } = default!;

			[field: SerializeField]
			public Transform LaunchPoint { get; private set; } = default!;

			[field: SerializeField]
			[Tooltip("Defines the X start and X end rotation for the arm.")]
			public Vector2 XRotation { get; private set; }
		}

		private void Awake()
		{
			PrepareProjectile();
		}

		private void PrepareProjectile()
		{
			_projectile = Instantiate(ProjectilePrefab, Arm.LaunchPoint.transform);

			// Ignoring collision between tower and projectile collider, otherwise the physics engine might move the
			// projectile due to the collider being in another collider.
			// We will not change the layer collision matrix here, because projectiles may collide with a tower
			// where we want to destroy it.
			Physics.IgnoreCollision(Tower.Collider, _projectile.Collider);
		}

		protected override UniTask LaunchProjectileAsync(Vector3 target, CancellationToken cancellationToken)
		{
			var launchVelocity = BallisticCalculationUtilities
				.CalculateBallisticArcVelocity(Arm.LaunchPoint.position, target, transform.forward);

			_projectile.EnsureOrThrow().Launch(launchVelocity);
			_projectile = null;

			return UniTask.CompletedTask;
		}

		protected override async UniTask StartAnimationAsync(CancellationToken cancellationToken) =>
			await Arm.ArmPivot
				.DOLocalRotate(new(Arm.XRotation.y, 0, 0), WeaponDefinition.LaunchAnimationTimeInSeconds)
				.SetEase(WeaponDefinition.LaunchEasing)
				.AwaitWithCancellation(cancellationToken);

		protected override async UniTask RewindAnimationAsync(CancellationToken cancellationToken) =>
			await Arm.ArmPivot
				.DOLocalRotate(new(Arm.XRotation.x, 0, 0), WeaponDefinition.RewindAnimationTimeInSeconds)
				.SetEase(WeaponDefinition.RewindEasing)
				.OnComplete(PrepareProjectile)
				.AwaitWithCancellation(cancellationToken);

		protected override void TrackTarget(TargetPoint target)
		{
			var direction = target.transform.position - transform.position;
			direction.y = 0;

			_targetRotation = Quaternion.LookRotation(direction);
		}

		protected override void Update()
		{
			base.Update();

			transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation,
				WeaponDefinition.RotationSpeedInDegreesPerSecond * Time.deltaTime);
		}

		protected override float CalculateLaunchAnimationDelay() => WeaponDefinition.LaunchAnimationTimeInSeconds + WeaponDefinition.RewindAnimationTimeInSeconds;
	}
}
