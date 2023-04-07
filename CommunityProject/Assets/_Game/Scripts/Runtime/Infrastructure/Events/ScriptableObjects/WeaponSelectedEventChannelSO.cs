using BoundfoxStudios.CommunityProject.Entities.Weapons;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Tower Selected Event Channel")]
	public class WeaponSelectedEventChannelSO : EventChannelSO<WeaponSelectedEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public Transform Transform;
			public ICanCalculateWeaponDefinition WeaponDefinition;
		}
	}
}
