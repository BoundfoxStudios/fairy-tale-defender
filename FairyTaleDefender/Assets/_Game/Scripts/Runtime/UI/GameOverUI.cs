using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace BoundfoxStudios.FairyTaleDefender.UI
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(GameOverUI))]
	public class GameOverUI : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private GameObject GamePlayCanvas { get; set; } = default!;

		[field: SerializeField]
		private LocalizedString PlayerWonText { get; set; } = default!;

		[field: SerializeField]
		private LocalizedString PlayerLostText { get; set; } = default!;

		[field: SerializeField]
		private SceneLoadRequester NextLevelButton { get; set; } = default!;

		[field: SerializeField]
		private GameObject LevelFinishedPanel { get; set; } = default!;

		[field: SerializeField]
		private LocalizeStringEvent LevelFinishLocalizedString { get; set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private LevelFinishedEventChannelSO LevelFinishedEventChannel { get; set; } = default!;

		private void OnEnable()
		{
			LevelFinishedEventChannel.Raised += InitDisplay;
			LevelFinishedPanel.gameObject.SetActive(false);
		}

		private void OnDisable()
		{
			LevelFinishedEventChannel.Raised -= InitDisplay;
		}


		private void InitDisplay(LevelFinishedEventChannelSO.EventArgs args)
		{
			SetLevelFinishedText(args.PlayerHasWon);
			SetupButtons();
			ActivateCanvases();
		}

		private void SetLevelFinishedText(bool playerWon)
		{
			LevelFinishLocalizedString.StringReference = playerWon ? PlayerWonText : PlayerLostText;
		}

		private void SetupButtons()
		{
			if (!NextLevelButton.HasNextLevel())
			{
				return;
			}

			//TODO: Only display next level button when it is unlocked.
			NextLevelButton.gameObject.SetActive(true);
		}

		private void ActivateCanvases()
		{
			LevelFinishedPanel.gameObject.SetActive(true);
			GamePlayCanvas.gameObject.SetActive(false);
		}
	}
}
