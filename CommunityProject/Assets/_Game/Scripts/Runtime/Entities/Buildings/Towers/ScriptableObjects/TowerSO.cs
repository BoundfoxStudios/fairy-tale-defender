using BoundfoxStudios.CommunityProject.Systems.BuildSystem;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Entities.Buildings.Towers.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Towers + "/Tower")]
	public class TowerSO : ScriptableObject, IBuildable
	{
		[field: SerializeField]
		public GameObject Prefab { get; private set; } = default!;

		[field: SerializeField]
		public GameObject BlueprintPrefab { get; private set; } = default!;
	}
}
