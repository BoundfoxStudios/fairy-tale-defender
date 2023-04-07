using BoundfoxStudios.FairyTaleDefender.Entities.Weapons;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Tower Selected Event Channel")]
	public class WeaponSelectedEventChannelSO : EventChannelSO<WeaponSelectedEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public Transform Transform;
			public ICanCalculateEffectiveWeaponDefinition EffectiveWeaponDefinition;
		}
	}
}
