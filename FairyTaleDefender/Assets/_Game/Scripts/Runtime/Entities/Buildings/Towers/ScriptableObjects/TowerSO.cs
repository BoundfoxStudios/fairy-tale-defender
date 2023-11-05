using UnityEngine;
using UnityEngine.Localization;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Buildings.Towers.ScriptableObjects
{
	public abstract class TowerSO : ScriptableObject
	{
		[field: SerializeField]
		public LocalizedString Name { get; private set; } = default!;
	}
}
