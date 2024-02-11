using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.BuildSystem;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Buildings.Towers.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Towers + "/Buildable Tower")]
	public class BuildableTowerSO : TowerSO, IAmBuildable, IHaveAPrice, IHaveBuildableUI
	{
		[field: Header("References")]
		[field: SerializeField]
		public GameObject Prefab { get; private set; } = default!;

		[field: SerializeField]
		public GameObject BlueprintPrefab { get; private set; } = default!;

		[field: SerializeField]
		public Sprite BuildableIcon { get; private set; } = default!;

		[field: SerializeField]
		public WeaponSO WeaponSO { get; private set; } = default!;

		[field: Header("Settings")]
		[field: SerializeField]
		[field: Range(1, 1000)]
		public int Price { get; private set; } = 100;
	}
}
