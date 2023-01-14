using BoundfoxStudios.CommunityProject.BuildSystem;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Buildings.Towers.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Towers + "/Tower")]
	public class TowerSO : ScriptableObject, IBuildable
	{
		[field: SerializeField]
		public GameObject Prefab { get; private set; }

		[field: SerializeField]
		public GameObject BlueprintPrefab { get; private set; }
	}
}
