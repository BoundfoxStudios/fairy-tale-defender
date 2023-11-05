using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Buildings.Towers.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Buildings.Towers
{
	[SelectionBase]
	[AddComponentMenu(Constants.MenuNames.Towers + "/" + nameof(Tower))]
	public class Tower : MonoBehaviour
	{
		[field: SerializeField]
		public TowerSO TowerDefinition { get; private set; } = default!;

		[field: SerializeField]
		public Collider Collider { get; private set; } = default!;
	}
}
