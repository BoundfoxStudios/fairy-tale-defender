using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeSets;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Characters.Enemies
{
	[AddComponentMenu(Constants.MenuNames.Characters + "/" + nameof(LivingEnemyManager))]
	public class LivingEnemyManager : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		public EnemyRuntimeSetSO LivingEnemies { get; private set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		public EnemyEventChannelSO EnemySpawnedEventChannel { get; private set; } = default!;

		[field: SerializeField]
		public EnemyEventChannelSO EnemyDestroyedEventChannel { get; private set; } = default!;

		private void OnEnable()
		{
			EnemySpawnedEventChannel.Raised += EnemySpawned;
			EnemyDestroyedEventChannel.Raised += EnemyDestroyed;
		}

		private void OnDisable()
		{
			EnemySpawnedEventChannel.Raised -= EnemySpawned;
			EnemyDestroyedEventChannel.Raised -= EnemyDestroyed;
		}

		private void EnemySpawned(Enemy spawnedEnemy)
		{
			LivingEnemies.Add(spawnedEnemy);
		}

		private void EnemyDestroyed(Enemy destroyedEnemy)
		{
			LivingEnemies.Remove(destroyedEnemy);
		}
	}
}
