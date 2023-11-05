using System;
using BoundfoxStudios.FairyTaleDefender.Infrastructure;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting.ScriptableObjects
{
	/// <summary>
	/// Target type for target by its health points.
	/// </summary>
	// We don't need more instances then enum members are available.
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
			var result = targets[0];
			var resultEnemy = result.GetComponent<TargetPoint>().Enemy;

			for (var i = 1; i < targets.Size; i++)
			{
				var target = targets[i];
				var enemy = target.GetComponent<TargetPoint>().Enemy;

				switch (LifeType)
				{
					case LifeTypes.Highest when enemy.Health.Current > resultEnemy.Health.Current:
					case LifeTypes.Lowest when enemy.Health.Current < resultEnemy.Health.Current:
						result = targets[i];
						resultEnemy = enemy;
						break;
				}
			}

			return result;
		}
	}
}
