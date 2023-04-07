using BoundfoxStudios.FairyTaleDefender.Systems.BuildSystem;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Buildings.Towers.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Towers + "/Tower")]
	public class TowerSO : ScriptableObject, IAmBuildable, IHaveAPrice
	{
		[field: SerializeField]
		public GameObject Prefab { get; private set; } = default!;

		[field: SerializeField]
		public GameObject BlueprintPrefab { get; private set; } = default!;

		[field: SerializeField]
		[field: Range(1, 1000)]
		public int Price { get; private set; } = 100;
	}
}
