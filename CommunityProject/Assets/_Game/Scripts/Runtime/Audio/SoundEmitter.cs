using System;
using BoundfoxStudios.CommunityProject.Audio.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Audio
{
	[AddComponentMenu(Constants.MenuNames.Audio + "/" + nameof(SoundEmitter))]
	public class SoundEmitter : MonoBehaviour
	{
		private AudioSource _audioSource;

		public event Action Finished;

		private void OnEnable()
		{
			_audioSource = gameObject.GetComponent<AudioSource>();
		}

		public void PlayAudioCue(AudioCueSO audioCue)
		{
			Play(audioCue).Forget();
		}

		private async UniTaskVoid Play(AudioCueSO audioCue)
		{
			_audioSource.clip = audioCue.AudioClip;
			_audioSource.Play();

			var clipLengthInSeconds = TimeSpan.FromSeconds(_audioSource.clip.length);

			await UniTask.Delay(clipLengthInSeconds, true);

			Finished?.Invoke();
		}
	}
}
