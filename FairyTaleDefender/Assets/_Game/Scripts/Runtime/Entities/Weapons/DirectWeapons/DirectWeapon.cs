using System;
using System.Threading;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons.Projectiles;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.DirectWeapons.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.DirectWeapons
{
	[AddComponentMenu(Constants.MenuNames.Weapons + "/" + nameof(DirectWeapon))]
	public class DirectWeapon : Weapon<DirectWeaponSO, DirectTargetLocatorSO, EffectiveDirectWeaponDefinition>
	{
		[field: SerializeField]
		private Projectile ProjectilePrefab { get; set; } = default!;

		[field: SerializeField]
		private ArmSettings Arm { get; set; } = default!;

		[field: SerializeField]
		[field: Tooltip("Lower number means a faster projectile.")]
		[field: Range(0.01f, 1f)]
		public float ProjectileTimeToReachTarget { get; private set; } = 0.1f;

		private Projectile? _projectile;
		private Quaternion _targetRotation;
		private Quaternion _targetArmRotation;

		[Serializable]
		public class ArmSettings
		{
			[field: SerializeField]
			public Transform ArmPivot { get; private set; } = default!;

			[field: SerializeField]
			public Transform LaunchPoint { get; private set; } = default!;
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
			var direction = Arm.LaunchPoint.forward;
			var distance = Vector3.Distance(Arm.LaunchPoint.position, target);

			if (_projectile.Exists())
			{
				_projectile.Launch(direction * (distance / ProjectileTimeToReachTarget), useGravity: false);
			}

			_projectile = null;

			return UniTask.CompletedTask;
		}

		protected override UniTask StartAnimationAsync(CancellationToken cancellationToken) => UniTask.CompletedTask;

		protected override async UniTask RewindAnimationAsync(CancellationToken cancellationToken)
		{
			await UniTask.WaitForSeconds(WeaponDefinition.RewindAnimationTimeInSeconds, cancellationToken: cancellationToken);
			PrepareProjectile();
		}

		protected override void TrackTarget(TargetPoint target)
		{
			// y-rotation
			var direction = target.Center - transform.position;
			direction.y = 0;

			_targetRotation = Quaternion.LookRotation(direction);

			// Arm x-rotation
			direction = target.Center - Arm.LaunchPoint.position;
			var armRotation = Quaternion.LookRotation(direction).eulerAngles.x;
			_targetArmRotation = Quaternion.Euler(armRotation, 0, 0);
		}

		protected override void Update()
		{
			base.Update();

			transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation,
				WeaponDefinition.RotationSpeedInDegreesPerSecond * Time.deltaTime);

			Arm.ArmPivot.localRotation = Quaternion.RotateTowards(Arm.ArmPivot.localRotation, _targetArmRotation,
				WeaponDefinition.RotationSpeedInDegreesPerSecond * Time.deltaTime);
		}
	}
}
