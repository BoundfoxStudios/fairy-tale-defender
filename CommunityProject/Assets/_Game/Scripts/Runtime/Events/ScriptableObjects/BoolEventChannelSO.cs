using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Events.ScriptableObjects
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
