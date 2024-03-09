using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement;
using BoundfoxStudios.FairyTaleDefender.Systems.SaveGameSystem.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI.Preparation.WorldMap
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(WorldMapLevelButtons))]
	public class WorldMapLevelButtons : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private SceneLoadRequester[] LevelButtons { get; set; } = default!;

		[field: SerializeField]
		private SaveGameRuntimeAnchorSO SaveGameRuntimeAnchor { get; set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private LoadSceneEventChannelSO NotifyColdStartupEventChannel { get; set; } = default!;

		private void Awake()
		{
			ActivateLevelButtons();
		}

		private void OnEnable()
		{
			NotifyColdStartupEventChannel.Raised += NotifyColdStartup;
		}

		private void OnDisable()
		{
			NotifyColdStartupEventChannel.Raised -= NotifyColdStartup;
		}

		private void NotifyColdStartup(LoadSceneEventChannelSO.EventArgs _)
		{
			ActivateLevelButtons();
		}

		private void ActivateLevelButtons()
		{
			var saveGame = SaveGameRuntimeAnchor.Item;

			// This will happen during cold startup.
			if (saveGame == null)
			{
				return;
			}

			foreach (var levelButton in LevelButtons)
			{
				levelButton.gameObject.SetActive(saveGame.Data.UnlockedLevels.Contains(levelButton.SceneToLoad));
			}
		}
	}
}
