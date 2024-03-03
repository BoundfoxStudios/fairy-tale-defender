using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.AudioSystem.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(fileName = Constants.FileNames.EventChannelSuffix,
		menuName = Constants.MenuNames.Events + "/Audio Cue" + Constants.MenuNames.EventChannelSuffix)]
	public class AudioCueEventChannelSO : EventChannelSO<AudioCueSO>
	{
		// Marker Class
	}
}
