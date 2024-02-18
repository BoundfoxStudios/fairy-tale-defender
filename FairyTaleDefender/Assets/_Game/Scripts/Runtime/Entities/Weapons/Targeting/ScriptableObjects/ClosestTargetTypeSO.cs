using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting.ScriptableObjects
{
	/// <summary>
	/// Target type for selecting closest target in relation to weapon.
	/// </summary>
	// We don't need more than one instance.
	// [CreateAssetMenu(fileName = "ClosestTargetType", menuName = Constants.MenuNames.Targeting + "/Closest Target Type")]
	public class ClosestTargetTypeSO : TargetTypeSO
	{
		public override Collider GetTargetNonAlloc(Vector3 weaponPosition, NoAllocArrayResult<Collider> targets)
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
