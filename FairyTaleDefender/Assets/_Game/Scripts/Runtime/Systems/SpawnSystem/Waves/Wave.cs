using System;
using System.Threading;
using BoundfoxStudios.FairyTaleDefender.Entities.Characters.Enemies;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Splines;

namespace BoundfoxStudios.FairyTaleDefender.Systems.SpawnSystem.Waves
{
	/// <summary>
	/// Represents a single wave of one enemy type.
	/// </summary>
	[Serializable]
	public abstract class Wave
	{
		[field: SerializeField]
		[field: Range(0, 5)]
		public float DelayBetweenEachSpawnInSeconds { get; private set; } = 1;

		[field: SerializeField]
		[field: Range(1, 50)]
		public int EnemiesToSpawn { get; private set; } = 10;

		public float TimeToSpawnAllEnemies => (EnemiesToSpawn - 1) * DelayBetweenEachSpawnInSeconds;

		protected EventChannelSO<Enemy>? EnemySpawnedEventChannel;

		public abstract UniTask SpawnAsync(ISpline spline, SplineContainer splineContainer,
			CancellationToken cancellationToken, EventChannelSO<Enemy> enemySpawnedEventChannel);

#if UNITY_EDITOR
		public abstract string InspectorName { get; }
		public abstract bool IsValid { get; }
#endif
	}
}
