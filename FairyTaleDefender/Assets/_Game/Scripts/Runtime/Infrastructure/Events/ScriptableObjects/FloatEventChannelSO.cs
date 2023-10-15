using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	/// <summary>
	/// Event channel with a float argument.
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Float Event Channel")]
	public class FloatEventChannelSO : EventChannelSO<float>
	{
		// Marker class
	}
}
