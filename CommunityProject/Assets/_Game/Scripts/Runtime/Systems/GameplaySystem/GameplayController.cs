using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Systems.GameplaySystem
{
	[AddComponentMenu(Constants.MenuNames.CameraSystem + "/" + nameof(GameplayController))]
	public class GameplayController : MonoBehaviour
	{
		[field: Header("Listening Channels")]
		[field: SerializeField]
		private VoidEventChannelSO SceneReadyEventChannel { get; set; } = default!;

		[field: SerializeField]
		private BoolEventChannelSO WaveSpawnedEventChannel { get; set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		private VoidEventChannelSO GameplayStartEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO SpawnNextWaveEventChannel { get; set; } = default!;

		private void OnEnable()
		{
			SceneReadyEventChannel.Raised += SceneReady;
			WaveSpawnedEventChannel.Raised += WaveSpawned;
		}

		private void OnDisable()
		{
			SceneReadyEventChannel.Raised -= SceneReady;
			WaveSpawnedEventChannel.Raised -= WaveSpawned;
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
