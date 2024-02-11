using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Level Finished Event Channel")]
	public class LevelFinishedEventChannelSO : EventChannelSO<LevelFinishedEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public bool PlayerHasWon;
		}
	}
}
