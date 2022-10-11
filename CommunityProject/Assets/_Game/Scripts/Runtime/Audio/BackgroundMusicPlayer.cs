using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Audio
{
	/// <summary>
	/// This component plays <see cref="AudioClip"/>s from a given <see cref="PlaylistSO"/> on the <see cref="AudioSource"/> on same GameObject.
	/// </summary>
	[RequireComponent(typeof(AudioSource))]
	public class BackgroundMusicPlayer : MonoBehaviour
	{
		[SerializeField]
		private PlaylistSO playlist;

		private AudioSource _audioSource;

		private async UniTaskVoid Awake()
		{
			_audioSource = GetComponent<AudioSource>();

			if (playlist == null)
			{
#if UNITY_EDITOR
				Debug.LogWarning("No Playlist on this BackgroundMusicPlayer", this);
#endif
				return;
			}

			await PlayMusicAsync();
		}

		private async UniTask PlayMusicAsync()
		{
			while (true)
			{
				var clip = GetNextClip();

				if (clip)
				{
					_audioSource.clip = clip;
					_audioSource.Play();

					await UniTask.Delay(TimeSpan.FromSeconds(clip.length), ignoreTimeScale: true);
				}
				await UniTask.Yield();
			}
		}

		private AudioClip GetNextClip()
		{
			return playlist.GetNextRandomClipWithoutImmediateRepeat();
		}
	}
}
