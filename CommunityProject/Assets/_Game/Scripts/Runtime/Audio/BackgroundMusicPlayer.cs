using BoundfoxStudios.CommunityProject.Audio.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Audio
{
	/// <summary>
	/// This component plays <see cref="AudioClip"/>s from a given <see cref="PlaylistSO"/> on the <see cref="AudioSource"/> on same GameObject.
	/// </summary>
	[RequireComponent(typeof(AudioSource))]
	[AddComponentMenu(Constants.MenuNames.Audio + "/" + nameof(BackgroundMusicPlayer))]
	public class BackgroundMusicPlayer : MonoBehaviour
	{
		[SerializeField]
		private PlaylistSO Playlist;

		private AudioSource _audioSource;

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();

			if (!Playlist)
			{
				Debug.LogError("No Playlist on this BackgroundMusicPlayer", this);

				return;
			}

			PlayMusicAsync().Forget();
		}

		private async UniTaskVoid PlayMusicAsync()
		{
			var clip = GetNextClip();

			if (!clip)
			{
				Debug.LogError("No valid Playlist", this);
				return;
			}

			_audioSource.clip = clip;
			_audioSource.Play();

			await UniTask.Delay(TimeSpan.FromSeconds(clip.length), ignoreTimeScale: true);

			PlayMusicAsync().Forget();
		}

		private AudioClip GetNextClip()
		{
			return Playlist.GetNextRandomClipWithoutImmediateRepeat();
		}
	}
}
