using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Buildings.Towers.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.BuildSystem.UI
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(TowerBuildButton))]
	public class TowerBuildButton : BuildButton<BuildableTowerSO>
	{
	}
}
