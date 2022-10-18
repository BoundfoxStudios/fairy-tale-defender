using BoundfoxStudios.CommunityProject.Audio.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Audio
{
	[AddComponentMenu(Constants.MenuNames.Audio + "/" + nameof(AudioCuePlayer))]
	public class AudioCuePlayer : MonoBehaviour
	{
		[SerializeField]
		private AudioCueEventChannelSO AudioCueEventChannel;

		[SerializeField]
		private SoundEmitter SoundEmitterPrefab;

		private void OnEnable()
		{
			AudioCueEventChannel.Raised += Play;
		}

		private void OnDisable()
		{
			AudioCueEventChannel.Raised -= Play;
		}

		private void Play(AudioCueSO audioCue)
		{
			var emitter = Instantiate(SoundEmitterPrefab, Vector3.zero, Quaternion.identity);

			emitter.PlayAudioCue(audioCue);

			emitter.Finished += () =>
			{
				Destroy(emitter.gameObject);
			};
		}
	}
}
