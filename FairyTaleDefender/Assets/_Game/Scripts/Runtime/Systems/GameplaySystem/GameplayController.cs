using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem
{
	[AddComponentMenu(Constants.MenuNames.GameplaySystem + "/" + nameof(GameplayController))]
	public class GameplayController : MonoBehaviour
	{
		// TODO: Will be removed later
		[field: Header("References")]
		[field: SerializeField]
		private MenuSO MainMenuScene { get; set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private VoidEventChannelSO SceneReadyEventChannel { get; set; } = default!;

		[field: SerializeField]
		private BoolEventChannelSO WaveSpawnedEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO GameOverEventChannel { get; set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		private VoidEventChannelSO GameplayStartEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO SpawnNextWaveEventChannel { get; set; } = default!;

		[field: SerializeField]
		private LoadSceneEventChannelSO LoadSceneEventChannel { get; set; } = default!;

		private void OnEnable()
		{
			SceneReadyEventChannel.Raised += SceneReady;
			WaveSpawnedEventChannel.Raised += WaveSpawned;
			GameOverEventChannel.Raised += GameOver;
		}

		private void OnDisable()
		{
			SceneReadyEventChannel.Raised -= SceneReady;
			WaveSpawnedEventChannel.Raised -= WaveSpawned;
			GameOverEventChannel.Raised -= GameOver;
		}

		private void GameOver()
		{
			// TODO: Will be removed later
			LoadSceneEventChannel.Raise(new() { Scene = MainMenuScene });
		}

		private void WaveSpawned(bool hasMoreWaves)
		{
			if (hasMoreWaves)
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
