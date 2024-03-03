using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	/// <summary>
	/// Event channel with a bool argument.
	/// </summary>
	[CreateAssetMenu(fileName = Constants.FileNames.EventChannelSuffix,
		menuName = Constants.MenuNames.Events + "/Bool" + Constants.MenuNames.EventChannelSuffix)]
	public class BoolEventChannelSO : EventChannelSO<bool>
	{
		// Marker class
	}
}
