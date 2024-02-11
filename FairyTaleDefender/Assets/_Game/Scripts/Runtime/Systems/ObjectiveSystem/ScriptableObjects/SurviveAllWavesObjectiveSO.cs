using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeSets;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.ObjectiveSystem.ScriptableObjects
{
	// We need only one instance of this.
	//[CreateAssetMenu(menuName = Constants.MenuNames.Objectives + "/Survive All Waves Objectives")]
	public class SurviveAllWavesObjectiveSO : ObjectiveSO
	{
		[field: Header("References")]
		[field: SerializeField]
		public EnemyRuntimeSetSO LivingEnemies { get; private set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		public WaveSpawnedEventChannelSO WaveSpawnedEventChannel { get; private set; } = default!;

		[field: SerializeField]
		public VoidEventChannelSO RuntimeSetChangedEventChannel { get; private set; } = default!;

		private bool _levelHasMoreWaves = true;

		private void OnEnable()
		{
			WaveSpawnedEventChannel.Raised += WaveSpawned;
			RuntimeSetChangedEventChannel.Raised += EnemyRuntimeSetChanged;
		}

		private void OnDisable()
		{
			WaveSpawnedEventChannel.Raised -= WaveSpawned;
			RuntimeSetChangedEventChannel.Raised -= EnemyRuntimeSetChanged;
		}

		private void WaveSpawned(WaveSpawnedEventChannelSO.EventArgs args)
		{
			_levelHasMoreWaves = args.LevelHasMoreWaves;
		}

		private void EnemyRuntimeSetChanged()
		{
			if (LevelHasMoreEnemies())
			{
				return;
			}

			Complete();
		}

		private bool LevelHasMoreEnemies()
		{
			return LivingEnemies.Items.Count > 0 || _levelHasMoreWaves;
		}
	}
}
