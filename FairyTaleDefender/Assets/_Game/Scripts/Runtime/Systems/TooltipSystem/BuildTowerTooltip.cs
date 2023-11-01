using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.ScriptableObjects;
using UnityEngine;
using UnityEngine.Localization;

namespace BoundfoxStudios.FairyTaleDefender.Systems.TooltipSystem
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(BuildTowerTooltip))]
	public class BuildTowerTooltip : Tooltip, IBuildTowerTooltip
	{
		[field: SerializeField]
		public LocalizedString TowerName { get; private set; } = default!;

		[field: SerializeField]
		public WeaponSO WeaponDefinition { get; private set; } = default!;
	}
}
