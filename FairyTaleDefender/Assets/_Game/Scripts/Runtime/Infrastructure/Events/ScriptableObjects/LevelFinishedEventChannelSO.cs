using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(fileName = Constants.FileNames.EventChannelSuffix,
		menuName = Constants.MenuNames.Events + "/Level Finished" + Constants.MenuNames.EventChannelSuffix)]
	public class LevelFinishedEventChannelSO : EventChannelSO<LevelFinishedEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public bool PlayerHasWon;
		}
	}
}
