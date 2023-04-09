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
		public abstract TargetPoint? Locate(Vector3 weaponPosition, Vector3 towerForward, TargetTypeSO targetType,
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
		protected TargetPoint? ByTargetTypeNonAlloc(Vector3 weaponPosition, NoAllocArrayResult<Collider> targets, TargetTypeSO targetType)
		{
			if (targets == 0)
			{
				return null;
			}

			var target = targetType.GetTargetNonAlloc(weaponPosition, targets);

			return target.GetComponent<TargetPoint>();
		}
	}
}
