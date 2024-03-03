using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	/// <summary>
	/// Event channel with a float argument.
	/// </summary>
	[CreateAssetMenu(fileName = Constants.FileNames.EventChannelSuffix,
		menuName = Constants.MenuNames.Events + "/Float" + Constants.MenuNames.EventChannelSuffix)]
	public class FloatEventChannelSO : EventChannelSO<float>
	{
		// Marker class
	}
}
