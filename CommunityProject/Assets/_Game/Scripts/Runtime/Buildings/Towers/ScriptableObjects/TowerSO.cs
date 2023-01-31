using BoundfoxStudios.CommunityProject.BuildSystem;
using BoundfoxStudios.CommunityProject.Infrastructure;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Buildings.Towers.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Towers + "/Tower")]
	public class TowerSO : ScriptableObject, IBuildable
	{
		[field: SerializeField]
		public GameObject Prefab { get; private set; } = default!;

		[field: SerializeField]
		public GameObject BlueprintPrefab { get; private set; } = default!;

		private void Awake()
		{
			Guard.AgainstNull(() => Prefab, this);
			Guard.AgainstNull(() => BlueprintPrefab, this);
		}
	}
}
