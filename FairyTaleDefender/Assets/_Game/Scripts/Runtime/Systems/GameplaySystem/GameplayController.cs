using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem
{
	[AddComponentMenu(Constants.MenuNames.GameplaySystem + "/" + nameof(GameplayController))]
	public class GameplayController : MonoBehaviour
	{
		[field: Header("References")]
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
