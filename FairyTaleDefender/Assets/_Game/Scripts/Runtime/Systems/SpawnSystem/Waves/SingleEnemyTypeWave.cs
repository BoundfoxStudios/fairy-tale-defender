using System;
using System.Threading;
using BoundfoxStudios.FairyTaleDefender.Entities.Characters.Enemies;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Object = UnityEngine.Object;

namespace BoundfoxStudios.FairyTaleDefender.Systems.SpawnSystem.Waves
{
	[Serializable]
	public class SingleEnemyTypeWave : Wave
	{
		[field: SerializeField]
		public Enemy EnemyPrefab { get; private set; } = default!;

		public override async UniTask SpawnAsync(ISpline spline, SplineContainer splineContainer,
			CancellationToken cancellationToken, EventChannelSO<Enemy> enemySpawnedEventChannel)
		{
			EnemySpawnedEventChannel = enemySpawnedEventChannel;

			var spawnedEnemies = 0;
			var delay = TimeSpan.FromSeconds(DelayBetweenEachSpawnInSeconds);

			splineContainer.Evaluate(spline, 0, out var position, out var tangent, out _);
			var rotation = tangent.Equals(float3.zero) ? Quaternion.identity : Quaternion.LookRotation(tangent);

			while (spawnedEnemies < EnemiesToSpawn)
			{
				var enemy = Object.Instantiate(EnemyPrefab, position, rotation);
				enemy.Initialize(spline);
				EnemySpawnedEventChannel.Raise(enemy);

				spawnedEnemies++;

				await UniTask.Delay(delay, cancellationToken: cancellationToken);
			}
		}

#if UNITY_EDITOR
		public override string InspectorName => EnemyPrefab ? EnemyPrefab.name : "No Enemy";

		public override bool IsValid => EnemyPrefab.Exists();
#endif
	}
}
