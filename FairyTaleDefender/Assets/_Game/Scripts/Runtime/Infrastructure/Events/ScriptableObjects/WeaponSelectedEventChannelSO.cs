using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Buildings.Towers.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(fileName = Constants.FileNames.EventChannelSuffix,
		menuName = Constants.MenuNames.Events + "/Tower Selected" + Constants.MenuNames.EventChannelSuffix)]
	public class WeaponSelectedEventChannelSO : EventChannelSO<WeaponSelectedEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public Transform Transform;
			public ICanCalculateEffectiveWeaponDefinition EffectiveWeaponDefinition;
			public TowerSO Tower;
		}
	}
}
