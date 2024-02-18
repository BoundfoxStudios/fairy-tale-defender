using BoundfoxStudios.FairyTaleDefender.Infrastructure;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting.ScriptableObjects
{
	/// <summary>
	/// Target type for selecting targets by their current health points.
	/// </summary>
	// We don't need more instances than enum members are available.
	// [CreateAssetMenu(fileName = "LifeTargetType", menuName = Constants.MenuNames.Targeting + "/Life Target Type")]
	public class LifeTargetTypeSO : TargetTypeSO
	{
		private enum LifeTypes
		{
			Highest,
			Lowest,
		}

		[field: SerializeField]
		private LifeTypes LifeType { get; set; }

		public override Collider GetTargetNonAlloc(Vector3 weaponPosition, NoAllocArrayResult<Collider> targets)
		{
			Debug.Assert(targets > 0, $"{nameof(targets.Size)} must be greater than 0.");

			var result = targets[0];

			if (targets == 1)
			{
				return result;
			}

			var resultEnemy = result.GetComponent<TargetPoint>().Enemy;

			for (var i = 1; i < targets; i++)
			{
				var target = targets[i];
				var enemy = target.GetComponent<TargetPoint>().Enemy;

				switch (LifeType)
				{
					case LifeTypes.Highest when enemy.Health.Current > resultEnemy.Health.Current:
					case LifeTypes.Lowest when enemy.Health.Current < resultEnemy.Health.Current:
						result = target;
						resultEnemy = enemy;
						break;
				}
			}

			return result;
		}
	}
}
