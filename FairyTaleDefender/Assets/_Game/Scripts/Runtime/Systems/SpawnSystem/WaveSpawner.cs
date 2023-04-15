using System.Collections.Generic;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.NavigationSystem.PathProviders;
using BoundfoxStudios.FairyTaleDefender.Systems.SpawnSystem.Waves;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.SpawnSystem
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
		private VoidEventChannelSO GameplayStartEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO SpawnNextWaveEventChannel { get; set; } = default!;

		[field: SerializeField]
		private BoolEventChannelSO WaveSpawnedEventChannel { get; set; } = default!;

		private Queue<Wave> _waves = new();

		private void OnEnable()
		{
			GameplayStartEventChannel.Raised += GameplayStart;
			SpawnNextWaveEventChannel.Raised += SpawnNextWave;
		}

		private void OnDisable()
		{
			GameplayStartEventChannel.Raised -= GameplayStart;
			SpawnNextWaveEventChannel.Raised -= SpawnNextWave;
		}

		private void GameplayStart()
		{
			PrepareWaveQueue();
		}

		private void PrepareWaveQueue()
		{
			_waves = new(LevelRuntimeAnchor.ItemSafe.Waves.Waves);
		}

		private void SpawnNextWave()
		{
			SpawnWaveAsync().Forget();
		}

		private async UniTask SpawnWaveAsync()
		{
			var pathProvider = new SplinePathProvider();
			var spline = pathProvider.CreatePath(WaySplineRuntimeAnchor.ItemSafe, new RandomSplineLinkDecisionMaker());
			var wave = _waves.Dequeue();
			await wave.SpawnAsync(spline, WaySplineRuntimeAnchor.ItemSafe, destroyCancellationToken);
			WaveSpawnedEventChannel.Raise(_waves.Count > 0);
		}
	}
}
