using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting.ScriptableObjects
{
	/// <summary>
	/// Target type for selecting a random target.
	/// </summary>
	// We don't need more than one instance.
	// [CreateAssetMenu(fileName = "RandomTargetType", menuName = Constants.MenuNames.Targeting + "/Random Target Type")]
	public class RandomTargetTypeSO : TargetTypeSO
	{
		public override Collider GetTargetNonAlloc(Vector3 weaponPosition, NoAllocArrayResult<Collider> targets)
		{
			{
				Debug.Assert(targets > 0, $"{nameof(targets.Size)} must be greater than 0.");

				return targets.Result.PickRandom(targets)!;
			}
		}
	}
}
