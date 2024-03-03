using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	/// <summary>
	/// Event channel without any arguments.
	/// </summary>
	[CreateAssetMenu(fileName = Constants.FileNames.EventChannelSuffix,
		menuName = Constants.MenuNames.Events + "/Void" + Constants.MenuNames.EventChannelSuffix)]
	public class VoidEventChannelSO : EventChannelSO
	{
		// Marker class
	}
}
