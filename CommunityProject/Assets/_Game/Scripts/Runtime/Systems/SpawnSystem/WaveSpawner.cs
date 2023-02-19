using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Systems.NavigationSystem.PathProviders;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Systems.SpawnSystem
{
	[AddComponentMenu(Constants.MenuNames.GameplaySystem + "/" + nameof(WaveSpawner))]
	public class WaveSpawner : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private LevelRuntimeAnchorSO LevelRuntimeAnchor { get; set; } = default!;

		[field: SerializeField]
		private WaySplineRuntimeAnchorSO WaySplineRuntimeAnchor { get; set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private VoidEventChannelSO SpawnNextWaveEventChannel { get; set; } = default!;

		[field: SerializeField]
		private BoolEventChannelSO WaveSpawnedEventChannel { get; set; } = default!;

		private int _nextWave;

		private void OnEnable()
		{
			SpawnNextWaveEventChannel.Raised += SpawnNextWave;
		}

		private void OnDisable()
		{
			SpawnNextWaveEventChannel.Raised -= SpawnNextWave;
		}

		private void SpawnNextWave()
		{
			SpawnWaveAsync(_nextWave).Forget();
			_nextWave++;
		}

		private async UniTask SpawnWaveAsync(int waveIndex)
		{
			var pathProvider = new SplinePathProvider();
			var spline = pathProvider.CreatePath(WaySplineRuntimeAnchor.ItemSafe, new RandomSplineLinkDecisionMaker());
			var wave = LevelRuntimeAnchor.ItemSafe.Waves.Waves[waveIndex];
			await wave.SpawnAsync(spline, WaySplineRuntimeAnchor.ItemSafe, destroyCancellationToken);
			WaveSpawnedEventChannel.Raise(waveIndex + 1 < LevelRuntimeAnchor.ItemSafe.Waves.Waves.Length);
		}
	}
}
