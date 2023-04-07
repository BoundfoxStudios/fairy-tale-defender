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
		public VoidEventChannelSO WeaponDeselectedEventChannel { get; private set; } = default!;

		private void OnEnable()
		{
			WeaponSelectedEventChannel.Raised += DisplayWeaponRange;
			WeaponDeselectedEventChannel.Raised += StopDisplayingWeaponRange;
		}

		private void OnDisable()
		{
			WeaponSelectedEventChannel.Raised -= DisplayWeaponRange;
			WeaponDeselectedEventChannel.Raised -= StopDisplayingWeaponRange;
		}
	}
}
