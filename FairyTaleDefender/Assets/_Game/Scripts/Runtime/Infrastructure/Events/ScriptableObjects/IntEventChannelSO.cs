using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	/// <summary>
	/// Event channel with an int argument.
	/// </summary>
	[CreateAssetMenu(fileName = Constants.FileNames.EventChannelSuffix,
		menuName = Constants.MenuNames.Events + "/Int" + Constants.MenuNames.EventChannelSuffix)]
	public class IntEventChannelSO : EventChannelSO<int>
	{
		// Marker class
	}
}
