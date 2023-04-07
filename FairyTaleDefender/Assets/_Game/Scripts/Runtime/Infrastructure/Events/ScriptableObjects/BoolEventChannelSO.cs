using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	/// <summary>
	/// Event channel with a bool argument.
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Bool Event Channel")]
	public class BoolEventChannelSO : EventChannelSO<bool>
	{
		// Marker class
	}
}
