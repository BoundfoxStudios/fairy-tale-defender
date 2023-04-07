using System;
using BoundfoxStudios.FairyTaleDefender.Systems.AudioSystem.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.AudioSystem
{
	/// <summary>
	/// This component plays <see cref="AudioClip"/>s from a given <see cref="PlaylistSO"/> on the <see cref="AudioSource"/> on same GameObject.
	/// </summary>
	[RequireComponent(typeof(AudioSource))]
	[AddComponentMenu(Constants.MenuNames.Audio + "/" + nameof(BackgroundMusicPlayer))]
	public class BackgroundMusicPlayer : MonoBehaviour
	{
		[field: SerializeField]
		private PlaylistSO Playlist { get; set; } = default!;

		private AudioSource _audioSource = default!;

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();

			PlayMusicAsync().Forget();
		}

		// ReSharper disable once FunctionRecursiveOnAllPaths
		// We want it to be recursive.
		private async UniTaskVoid PlayMusicAsync()
		{
			var clip = GetNextClip();

			_audioSource.clip = clip;
			_audioSource.Play();

			await UniTask.Delay(TimeSpan.FromSeconds(clip.length), ignoreTimeScale: true);

			PlayMusicAsync().Forget();
		}

		private AudioClip GetNextClip() => Playlist.GetNextRandomClipWithoutImmediateRepeat();
	}
}
