using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure;
using BoundfoxStudios.FairyTaleDefender.Systems.NavigationSystem;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting.ScriptableObjects
{
	/// <summary>
	/// Target type for selecting targets depending on the time it will take them to reach the end of their spline.
	/// </summary>
	// We don't need more instances than enum members.
	[CreateAssetMenu(fileName = "TimeToLevelGoalTargetType", menuName = Constants.MenuNames.Targeting + "/Time to Level Goal Target Type")]
	public class TimeToLevelGoalTargetTypeSO : TargetTypeSO
	{
		private enum TimeTypes
		{
			First,
			Last
		}

		[field: SerializeField]
		private TimeTypes TimeType { get; set; }

		public override Collider GetTargetNonAlloc(Vector3 weaponPosition, NoAllocArrayResult<Collider> targets)
		{
			Debug.Assert(targets > 0, $"{nameof(targets.Size)} must be greater than 0.");

			var result = targets[0];

			if (targets == 1)
			{
				return result;
			}

			var resultTimeToReachTarget = result.GetComponentInParent<SplineWalker>().GetTimeToReachSplineEnd();

			for (var i = 1; i < targets; i++)
			{
				var potentialTarget = targets[i];
				var timeToReachTarget = potentialTarget.GetComponentInParent<SplineWalker>().GetTimeToReachSplineEnd();

				switch (TimeType)
				{
					case TimeTypes.First when timeToReachTarget < resultTimeToReachTarget:
					case TimeTypes.Last when timeToReachTarget > resultTimeToReachTarget:
						result = potentialTarget;
						resultTimeToReachTarget = timeToReachTarget;
						break;
				}
			}

			return result;
		}
	}
}
