using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.SaveGameSystem.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem
{
	[AddComponentMenu(Constants.MenuNames.GameplaySystem + "/" + nameof(GameplayController))]
	public class GameplayController : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private AllLevelPacksSO AllLevelPacks { get; set; } = default!;

		[field: SerializeField]
		private SaveGameRuntimeAnchorSO SaveGameRuntimeAnchor { get; set; } = default!;

		[field: SerializeField]
		private SaveGameManagerSO SaveGameManager { get; set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private VoidEventChannelSO SceneReadyEventChannel { get; set; } = default!;

		[field: SerializeField]
		private WaveSpawnedEventChannelSO WaveSpawnedEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO AllObjectivesCompletedEventChannel { get; set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		private VoidEventChannelSO GameplayStartEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO SpawnNextWaveEventChannel { get; set; } = default!;

		[field: SerializeField]
		private LevelFinishedEventChannelSO LevelFinishedEventChannel { get; set; } = default!;

		private void OnEnable()
		{
			SceneReadyEventChannel.Raised += SceneReady;
			WaveSpawnedEventChannel.Raised += WaveSpawned;
			AllObjectivesCompletedEventChannel.Raised += ObjectivesCompleted;
		}

		private void OnDisable()
		{
			SceneReadyEventChannel.Raised -= SceneReady;
			WaveSpawnedEventChannel.Raised -= WaveSpawned;
			AllObjectivesCompletedEventChannel.Raised -= ObjectivesCompleted;
		}

		private void ObjectivesCompleted()
		{
			FinishLevel(true);
		}

		private void FinishLevel(bool playerWon)
		{
			LevelFinishedEventChannel.Raise(new()
			{
				PlayerHasWon = playerWon
			});

			// TODO: Possibly move this somewhere else
			// TODO: We need the information which level is currently being played.
			var currentLevelIdentity = SaveGameRuntimeAnchor.ItemSafe.Data.LastLevel;
			var currentLevel = AllLevelPacks.FindByIdentity(currentLevelIdentity!);

			if (currentLevel.Exists() && currentLevel.NextLevel.Exists())
			{
				SaveGameRuntimeAnchor.ItemSafe.Data.UnlockedLevels.Add(currentLevel.NextLevel);
			}

			SaveGameManager.SaveGameAsync(SaveGameRuntimeAnchor.ItemSafe).Forget();
		}

		private void WaveSpawned(WaveSpawnedEventChannelSO.EventArgs args)
		{
			if (args.LevelHasMoreWaves)
			{
				SpawnNextWaveEventChannel.Raise();
			}
		}

		private void SceneReady()
		{
			GameplayStartEventChannel.Raise();
			SpawnNextWaveEventChannel.Raise();
		}
	}
}
