using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	/// <summary>
	/// Event that is raised whenever a wave has spawned all enemies.
	/// </summary>
	/// <remarks>
	/// Indicates if a level has more waves to spawn in <see cref="EventArgs"/>
	/// </remarks>
	[CreateAssetMenu(fileName = "WaveSpawned_EventChannel", menuName = Constants.MenuNames.Events + "/Wave Spawned Event Channel", order = 0)]
	public class WaveSpawnedEventChannelSO : EventChannelSO<WaveSpawnedEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public bool LevelHasMoreWaves;
		}
	}
}
