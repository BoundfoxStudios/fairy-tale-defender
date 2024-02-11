using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.SaveGameSystem;
using BoundfoxStudios.FairyTaleDefender.Systems.SaveGameSystem.ScriptableObjects;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.FairyTaleDefender.UI.MainMenu
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(StartContinueButtons))]
	public class StartContinueButtons : MonoBehaviour
	{
		[field: SerializeField]
		private Button StartGameButton { get; set; } = default!;

		[field: SerializeField]
		private Button ContinueGameButton { get; set; } = default!;

		[field: SerializeField]
		private SaveGameManagerSO SaveGameManager { get; set; } = default!;

		[field: SerializeField]
		private AllLevelPacksSO AllLevelPacks { get; set; } = default!;

		[field: SerializeField]
		private SaveGameRuntimeAnchorSO SaveGameRuntimeAnchor { get; set; } = default!;

		[field: SerializeField]
		private LoadSceneEventChannelSO LoadSceneEventChannel { get; set; } = default!;

		[field: SerializeField]
		private MenuSO PreparationScene { get; set; } = default!;

		// ReSharper disable once Unity.IncorrectMethodSignature
		[UsedImplicitly]
		private async UniTaskVoid Start()
		{
			var saveGames = await SaveGameManager.ListAvailableSaveGamesAsync();

			ContinueGameButton.gameObject.SetActive(
				saveGames.SingleOrDefault(saveGame => saveGame.Name == Constants.SaveGames.DefaultSaveGameName) != null);
			StartGameButton.gameObject.SetActive(saveGames.Count == 0);
		}

		public void StartNewGame()
		{
			StartNewGameAsync().Forget();
		}

		private async UniTaskVoid StartNewGameAsync()
		{
			StartGameButton.interactable = false;

			try
			{
				var saveGame = await SaveGameManager.CreateSaveGameAsync(Constants.SaveGames.DefaultSaveGameName, new()
				{
					LastLevel = AllLevelPacks.LevelPacks[0].Levels[0]
				});

				// TODO: Needs error handling when save game is null

				GoToPreparation(saveGame!);
			}
			finally
			{
				StartGameButton.interactable = true;
			}
		}

		public void ContinueGame()
		{
			ContinueGameAsync().Forget();
		}

		private async UniTaskVoid ContinueGameAsync()
		{
			ContinueGameButton.interactable = false;

			try
			{
				var saveGames = await SaveGameManager.ListAvailableSaveGamesAsync();

				var saveGameMeta = saveGames.Single(saveGame => saveGame.Name == Constants.SaveGames.DefaultSaveGameName);
				var saveGame = await SaveGameManager.LoadSaveGameAsync(saveGameMeta);

				// TODO: Needs error handling when save game is null

				GoToPreparation(saveGame!);
			}
			finally
			{
				ContinueGameButton.interactable = true;
			}
		}

		private void GoToPreparation(SaveGame saveGame)
		{
			SaveGameRuntimeAnchor.Item = saveGame;
			LoadSceneEventChannel.Raise(new()
			{
				ShowLoadingScreen = true,
				Scene = PreparationScene,
			});
		}
	}
}
