using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Buildings.Towers.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.TooltipSystem
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(BuildTowerTooltip))]
	public class BuildTowerTooltip : Tooltip, IBuildTowerTooltip
	{
		[field: SerializeField]
		public WeaponSO WeaponDefinition { get; private set; } = default!;

		[field: SerializeField]
		public TowerSO TowerDefinition { get; private set; } = default!;
	}
}
