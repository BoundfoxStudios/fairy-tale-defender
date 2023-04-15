using BoundfoxStudios.FairyTaleDefender.Common;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.FairyTaleDefender.UI.Utility
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(Bar))]
	public class Bar : MonoBehaviour
	{
		[field: SerializeField]
		private Image ForegroundBar { get; set; } = default!;

		[field: SerializeField]
		private Image BackgroundBar { get; set; } = default!;

		[field: SerializeField]
		private float AnimationSpeed = 0.333f;

		private float _maximumValue;
		private float _value;

		public float Value
		{
			get => _value;
			set
			{
				var delta = _value - value;
				_value = Mathf.Clamp(value, 0, _maximumValue);

				UpdateBarsAnimated(delta);
			}
		}

		public void Initialize(float value, float maximumValue)
		{
			_maximumValue = maximumValue;
			_value = Mathf.Clamp(value, 0, maximumValue);
			UpdateBarsInstant();
		}

		private float NormalizedValue => Value / _maximumValue;

		private void UpdateBarsAnimated(float delta)
		{
			var directChangeBar = delta <= 0 ? BackgroundBar : ForegroundBar;
			var animateChangeBar = delta <= 0 ? ForegroundBar : BackgroundBar;

			var normalizedValue = NormalizedValue;

			directChangeBar.fillAmount = normalizedValue;
			animateChangeBar
				.DOFillAmount(normalizedValue, AnimationSpeed)
				.WithCancellation(destroyCancellationToken);
		}

		private void UpdateBarsInstant()
		{
			var normalizedValue = NormalizedValue;
			ForegroundBar.fillAmount = normalizedValue;
			BackgroundBar.fillAmount = normalizedValue;
		}
	}
}
