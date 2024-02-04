using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace BoundfoxStudios.FairyTaleDefender
{
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
	    private TextMeshProUGUI LevelFinishText { get; set; } = default!;

	    [field: SerializeField]
	    private SceneLoadRequester NextLevelButton { get; set; } = default!;

	    [field: SerializeField]
	    private GameObject LevelFinishedPanel { get; set; } = default!;

	    [field: Header("Listening Channels")]
	    [field: SerializeField]
	    private BoolEventChannelSO LevelFinishedEventChannel { get; set; } = default!;

	    private void OnEnable()
	    {
		    LevelFinishedEventChannel.Raised += InitDisplay;
		    LevelFinishedPanel.gameObject.SetActive(false);
	    }

	    private void OnDisable()
	    {
		    LevelFinishedEventChannel.Raised -= InitDisplay;
	    }

	    private void InitDisplay(bool playerWon)
	    {
		    SetLevelFinishedText(playerWon);
		    SetupButtons();
		    ActivateCanvases();
	    }

	    private void SetLevelFinishedText(bool playerWon)
	    {
		    LevelFinishText.text = playerWon ? PlayerWonText.GetLocalizedString() : PlayerLostText.GetLocalizedString();
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
