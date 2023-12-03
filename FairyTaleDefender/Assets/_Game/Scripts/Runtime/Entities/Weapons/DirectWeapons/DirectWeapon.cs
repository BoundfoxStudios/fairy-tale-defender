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
		private GameObject LaunchPoint { get; set; } = default!;

		private Projectile? _projectile;
		private Quaternion _targetRotation;

		private void Awake()
		{
			PrepareProjectile();
		}

		private void PrepareProjectile()
		{
			_projectile = Instantiate(ProjectilePrefab, LaunchPoint.transform);

			// Ignoring collision between tower and projectile collider, otherwise the physics engine might move the
			// projectile due to the collider being in another collider.
			// We will not change the layer collision matrix here, because projectiles may collide with a tower
			// where we want to destroy it.
			Physics.IgnoreCollision(Tower.Collider, _projectile.Collider);
		}

		protected override UniTask LaunchProjectileAsync(Vector3 target, CancellationToken cancellationToken)
		{
			var weaponPosition = transform.position;
			var direction = (weaponPosition - target).normalized;
			var distance = Vector3.Distance(weaponPosition, target);

			if (_projectile.Exists())
			{
				_projectile.transform.LookAt(target);
				_projectile.Launch(-direction * (distance / 0.1f), useGravity: false);
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
			var direction = target.Center - transform.position;
			direction.y = 0;

			_targetRotation = Quaternion.LookRotation(direction);
		}

		protected override void Update()
		{
			base.Update();

			transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation,
				WeaponDefinition.RotationSpeedInDegreesPerSecond * Time.deltaTime);
		}
	}
}
