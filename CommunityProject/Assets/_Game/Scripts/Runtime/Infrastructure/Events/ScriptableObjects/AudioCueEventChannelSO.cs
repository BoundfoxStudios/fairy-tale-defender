using BoundfoxStudios.CommunityProject.Systems.AudioSystem.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Audio Cue Event Channel")]
	public class AudioCueEventChannelSO : EventChannelSO<AudioCueSO>
	{
		// Marker Class
	}
}
