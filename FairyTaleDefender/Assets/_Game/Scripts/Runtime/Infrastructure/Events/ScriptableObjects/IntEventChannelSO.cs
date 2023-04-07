using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects
{
	/// <summary>
	/// Event channel with an int argument.
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Int Event Channel")]
	public class IntEventChannelSO : EventChannelSO<int>
	{
		// Marker class
	}
}
