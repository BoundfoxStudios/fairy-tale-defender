using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(PauseMenuUI))]
	public class PauseMenuUI : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private GameObject PauseMenu { get; set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private BoolEventChannelSO TogglePauseEventChannel { get; set; } = default!;

		private void OnEnable()
		{
			TogglePauseEventChannel.Raised += TogglePauseMenu;
		}

		private void OnDisable()
		{
			TogglePauseEventChannel.Raised -= TogglePauseMenu;
		}

		private void TogglePauseMenu(bool paused)
		{
			PauseMenu.SetActive(paused);
		}
	}
}
