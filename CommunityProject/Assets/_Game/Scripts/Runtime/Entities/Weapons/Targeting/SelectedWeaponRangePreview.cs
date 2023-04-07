using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Entities.Weapons.Targeting
{
	[AddComponentMenu(Constants.MenuNames.Targeting + "/" + nameof(SelectedWeaponRangePreview))]
	public class SelectedWeaponRangePreview : WeaponRangePreview
	{
		[field: Header("Listening Event Channels")]
		[field: SerializeField]
		public WeaponSelectedEventChannelSO WeaponSelectedEventChannel { get; private set; } = default!;

		[field: SerializeField]
		public VoidEventChannelSO WeaponDeselectEventChannel { get; private set; } = default!;

		private void OnEnable()
		{
			WeaponSelectedEventChannel.Raised += DisplayWeaponRange;
			WeaponDeselectEventChannel.Raised += StopDisplayingWeaponRange;
		}

		private void OnDisable()
		{
			WeaponSelectedEventChannel.Raised -= DisplayWeaponRange;
			WeaponDeselectEventChannel.Raised -= StopDisplayingWeaponRange;
		}
	}
}
