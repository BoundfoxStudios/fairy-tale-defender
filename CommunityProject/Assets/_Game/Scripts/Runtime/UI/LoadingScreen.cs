using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.UI
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(LoadingScreen))]
	public class LoadingScreen : MonoBehaviour
	{
		[Header("Settings")]
		[SerializeField]
		[Min(0)]
		private float FadeInOutTime = 0.133f;

		[Header("References")]
		[SerializeField]
		private CanvasGroup LoadingScreenCanvasGroup;

		[Header("Listening Channels")]
		[SerializeField]
		private BoolEventChannelSO ToggleLoadingScreenEventChannel;

		private Tween _runningTween;

		private void Awake()
		{
			LoadingScreenCanvasGroup.alpha = 0;
		}

		private void OnEnable()
		{
			ToggleLoadingScreenEventChannel.Raised += ToggleLoadingScreen;
		}

		private void OnDisable()
		{
			ToggleLoadingScreenEventChannel.Raised -= ToggleLoadingScreen;
		}

		private void ToggleLoadingScreen(bool doShow)
		{
			ToggleLoadingScreenAsync(doShow).Forget();
		}

		private async UniTaskVoid ToggleLoadingScreenAsync(bool doShow)
		{
			if (_runningTween != null)
			{
				await _runningTween.AwaitForComplete();
			}

			var startValue = doShow ? 0 : 1;
			var endValue = doShow ? 1 : 0;

			_runningTween = LoadingScreenCanvasGroup
				.DOFade(endValue, FadeInOutTime)
				.From(startValue)
				.OnKill(() => _runningTween = null)
				.Play();
		}
	}
}
