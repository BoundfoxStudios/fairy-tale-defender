using System;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting.ScriptableObjects
{
	public abstract class TargetLocatorSO<T> : ScriptableObject
		where T : EffectiveWeaponDefinition
	{
		[field: SerializeField]
		private LayerMask EnemyLayerMask { get; set; }

		/// <summary>
		/// Returns a single target that is reachable by the <paramref name="weaponDefinition"/>.
		/// </summary>
		public abstract TargetPoint? Locate(Vector3 weaponPosition, Vector3 towerForward, TargetType targetType,
			T weaponDefinition);

		/// <summary>
		/// Checks if a certain <paramref name="targetPosition"/> is in an attackable range of the weapon.
		/// </summary>
		public abstract bool IsInAttackRange(Vector3 weaponPosition, Vector3 targetPosition, Vector3 towerForward,
			T weaponDefinition);

		/// <summary>
		/// This is a cache that is used for OverlapSphereNonAlloc.
		/// It's currently set to size 50 which means that we can not target more than 50 targets at once.
		/// But for our game that should be enough.
		/// </summary>
		private readonly Collider[] _targetPointsCache = new Collider[50];

		/// <summary>
		/// Does an OverlapSphere to get all targets around the tower.
		/// It does not check, if the target is in attack range.
		/// </summary>
		protected NoAllocArrayResult<Collider> LocateAllInRangeNonAlloc(Vector3 position, float range)
		{
			var size = Physics.OverlapSphereNonAlloc(position, range, _targetPointsCache, EnemyLayerMask);
			return new()
			{
				Size = size,
				Result = _targetPointsCache
			};
		}

		/// <summary>
		/// Given an array of possible targets, the method will return a specific one depending on the <paramref name="targetType"/>.
		/// </summary>
		protected TargetPoint? ByTargetTypeNonAlloc(Vector3 weaponPosition, NoAllocArrayResult<Collider> targets, TargetType targetType)
		{
			if (targets == 0)
			{
				return null;
			}

			var target = targetType switch
			{
				TargetType.Closest => ByClosestTargetTypeNonAlloc(weaponPosition, targets),
				TargetType.Random => ByRandomTargetTypeNonAlloc(targets),
				_ => throw new ArgumentOutOfRangeException(nameof(targetType), $"{targetType} is not implemented yet.")
			};

			return target.GetComponent<TargetPoint>();
		}

		private Collider ByRandomTargetTypeNonAlloc(NoAllocArrayResult<Collider> targets)
		{
			Debug.Assert(targets > 0, $"{nameof(targets.Size)} must be greater than 0.");

			return targets.Result.PickRandom(targets)!;
		}

		private Collider ByClosestTargetTypeNonAlloc(Vector3 weaponPosition, NoAllocArrayResult<Collider> targets)
		{
			Debug.Assert(targets > 0, $"{nameof(targets.Size)} must be greater than 0.");

			var closestCollider = targets[0];

			if (targets == 1)
			{
				return closestCollider;
			}

			var smallestDistance = float.PositiveInfinity;

			for (var i = 0; i < targets; i++)
			{
				var target = targets[i];

				// Using the squared distance here to avoid using sqrt.
				var distanceSquared = weaponPosition.DistanceSquaredTo(target.transform.position);

				if (distanceSquared < smallestDistance)
				{
					smallestDistance = distanceSquared;
					closestCollider = target;
				}
			}

			return closestCollider;
		}
	}
}
