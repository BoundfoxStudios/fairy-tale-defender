using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	/// <summary>
	/// Event channel with an int delta argument.
	/// </summary>
	[CreateAssetMenu(fileName = Constants.FileNames.EventChannelSuffix,
		menuName = Constants.MenuNames.Events + "/Int Delta" + Constants.MenuNames.EventChannelSuffix)]
	public class IntDeltaEventChannelSO : EventChannelSO<IntDeltaEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public int Value;
			public int Delta;
		}
	}
}
