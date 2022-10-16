using System;
using BoundfoxStudios.CommunityProject.Audio.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Audio
{
    public class SoundEmitter : MonoBehaviour
    {
	    private AudioSource _audioSource;

	    public event Action Finished;

        // Start is called before the first frame update
        void Start()
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

	        await UniTask.Delay((int)_audioSource.clip.length, ignoreTimeScale: true);

	        Finished?.Invoke();
        }
    }
}
