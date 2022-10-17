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

        }

        private void OnEnable()
        {
	        _audioSource = gameObject.GetComponent<AudioSource>();
        }

        public void PlayAudioCue(AudioCueSO audioCue)
        {
	        Debug.Log("SoundEmitter Play");
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
