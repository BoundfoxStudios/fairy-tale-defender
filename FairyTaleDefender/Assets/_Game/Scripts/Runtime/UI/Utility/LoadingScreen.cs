using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI.Utility
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(LoadingScreen))]
	public class LoadingScreen : MonoBehaviour
	{
		[field: Header("Settings")]
		[field: SerializeField]
		[field: Min(0)]
		private float FadeInOutTime { get; set; } = 0.133f;

		[field: Header("References")]
		[field: SerializeField]
		private CanvasGroup LoadingScreenCanvasGroup { get; set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private BoolEventChannelSO ToggleLoadingScreenEventChannel { get; set; } = default!;

		private Tween? _runningTween;

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
