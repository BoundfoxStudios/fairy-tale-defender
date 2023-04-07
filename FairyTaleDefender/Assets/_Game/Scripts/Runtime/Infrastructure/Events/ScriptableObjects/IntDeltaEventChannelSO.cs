using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	/// <summary>
	/// Event channel with an int delta argument.
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Int Delta Event Channel")]
	public class IntDeltaEventChannelSO : EventChannelSO<IntDeltaEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public int Value;
			public int Delta;
		}
	}
}
