using System;
using BoundfoxStudios.CommunityProject.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Audio
{
	[AddComponentMenu(Constants.MenuNames.Audio + "/" + nameof(SoundEmitter))]
	[RequireComponent(typeof(AudioSource))]
	public class SoundEmitter : MonoBehaviour
	{
		private AudioSource _audioSource = default!;

		public event Action? Finished;

		private void Awake()
		{
			_audioSource = gameObject.GetComponentSafe<AudioSource>();
		}

		public void PlayAudioClip(AudioClip audioClip)
		{
			PlayAsync(audioClip).Forget();
		}

		private async UniTaskVoid PlayAsync(AudioClip audioClip)
		{
			_audioSource.clip = audioClip;
			_audioSource.Play();

			var clipLengthInSeconds = TimeSpan.FromSeconds(_audioSource.clip.length);

			await UniTask.Delay(clipLengthInSeconds, true);

			Finished?.Invoke();
		}
	}
}
