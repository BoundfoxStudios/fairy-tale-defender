using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using BoundfoxStudios.CommunityProject.Buildings.Towers;
using BoundfoxStudios.CommunityProject.Weapons.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Weapons.Targeting;
using BoundfoxStudios.CommunityProject.Weapons.Targeting.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Weapons
{
	[SelectionBase]
	public abstract class Weapon<T> : MonoBehaviour
		where T : WeaponSO
	{
		[field: SerializeField]
		public T WeaponDefinition { get; private set; } = default!;

		[SerializeField]
		protected TargetLocator<T> TargetLocator = default!;

		[field: SerializeField]
		public Tower Tower { get; private set; } = default!;

		[field: SerializeField]
		public TargetType TargetType { get; private set; }

		/// <summary>
		/// Launches the actual projectile to the target.
		/// </summary>
		/// <returns></returns>
		protected abstract UniTask LaunchProjectileAsync(Vector3 target, CancellationToken cancellationToken);

		/// <summary>
		/// Starts the weapon launch animation.
		/// </summary>
		protected abstract UniTask StartAnimationAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Animation that should be played after launching the projectile.
		/// </summary>
		protected abstract UniTask RewindAnimationAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Use this method to track a target, e.g. rotate the weapon towards the target.
		/// </summary>
		protected abstract void TrackTarget(TargetPoint target);

		private TargetPoint? _currentTarget;
		private Vector3 _towerForward;

		protected void Start()
		{
			_towerForward = Tower.transform.forward;

			StartLaunchSequenceAsync().Forget();
		}

		protected virtual void Update()
		{
			if (!_currentTarget && !TryAcquireTarget(out _currentTarget))
			{
				return;
			}

			var isTargetInRangeAndAlive = IsTargetInRangeAndAlive(_currentTarget);

			if (!isTargetInRangeAndAlive)
			{
				_currentTarget = null;
				return;
			}

			TrackTarget(_currentTarget!);
		}

		private bool IsTargetInRangeAndAlive(TargetPoint? target) =>
			target is not null && target && TargetLocator.IsInAttackRange(transform.position, target.transform.position, _towerForward,
				WeaponDefinition);

		private bool TryAcquireTarget([NotNullWhen(true)] out TargetPoint? currentTarget)
		{
			currentTarget = TargetLocator.Locate(transform.position, _towerForward, TargetType, WeaponDefinition);
			return currentTarget;
		}

		/// <summary>
		/// This is the main sequence to launch a projectile to a target.
		/// </summary>
		private async UniTaskVoid StartLaunchSequenceAsync()
		{
			var token = destroyCancellationToken;

			// First get the launch delay (fire rate) to wait before we can launch a projectile
			var launchDelay = CalculateLaunchDelay();
			await UniTask.Delay(launchDelay, cancellationToken: token);

			// If we don't have a target, we wait until we have a target, so we can launch a projectile immediately.
			if (!_currentTarget)
			{
				await UniTask.WaitUntil(() => _currentTarget, cancellationToken: token);
			}

			var targetPosition = _currentTarget!.transform.position;

			// Note: It could be that between start animation and launch projectile the current target is destroyed.
			// In that case we still launch the projectile to the last known position.
			await ExecuteIfNotCancelledAsync(token, StartAnimationAsync);
			await ExecuteIfNotCancelledAsync(token, async t => await LaunchProjectileAsync(targetPosition, t));
			await ExecuteIfNotCancelledAsync(token, RewindAnimationAsync);

			if (!token.IsCancellationRequested)
			{
				// Recursive call of this function to start another launch sequence.
				StartLaunchSequenceAsync().Forget();
			}
		}

		private async UniTask ExecuteIfNotCancelledAsync(CancellationToken token, Func<CancellationToken, UniTask> action)
		{
			if (token.IsCancellationRequested)
			{
				return;
			}

			await action(token);
		}

		private TimeSpan CalculateLaunchDelay()
		{
			var delay = WeaponDefinition.FireRateInSeconds - CalculateLaunchAnimationDelay();

			Debug.Assert(delay > 0, $"{nameof(CalculateLaunchDelay)} returns delay {delay} that is smaller or equal than 0, that is not valid!");

			return TimeSpan.FromSeconds(delay);
		}

		protected virtual float CalculateLaunchAnimationDelay() => 0;
	}
}
