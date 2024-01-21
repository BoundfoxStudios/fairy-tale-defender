using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Buildings.Towers.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeSets
{
	[CreateAssetMenu(fileName = "BuildableTower_RuntimeSet", menuName = Constants.MenuNames.RuntimeSets + "/" + nameof(BuildableTowerRuntimeSetSO))]
	public class BuildableTowerRuntimeSetSO : RuntimeSetSO<BuildableTowerSO>
	{
	}
}
