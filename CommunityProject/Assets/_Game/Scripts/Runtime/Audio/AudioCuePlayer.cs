using System;
using BoundfoxStudios.CommunityProject.Audio.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace BoundfoxStudios.CommunityProject.Audio
{
	public class AudioCuePlayer : MonoBehaviour
	{
		[SerializeField]
		private AudioCueEventChannelSO audioCueEventChannel;

		[SerializeField]
		private GameObject soundEmitterPrefab;

		private void OnEnable()
		{
			audioCueEventChannel.Raised += OnPlay;
		}

		private void OnDisable()
		{
			audioCueEventChannel.Raised -= OnPlay;
		}

		private void OnPlay(AudioCueSO audioCue)
		{
			var emitterObject = Instantiate(soundEmitterPrefab, Vector3.zero, Quaternion.identity);
			var emitter = emitterObject.GetComponent<SoundEmitter>();

			emitter.PlayAudioCue(audioCue);

			emitter.Finished += () =>
			{
				Destroy(emitterObject);
			};
		}


	}
}
