using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BoundfoxStudios.FairyTaleDefender.UI.Utility
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(SliderWithValue))]
	public class SliderWithValue : MonoBehaviour
	{
		[field: SerializeField]
		private TMP_InputField InputField { get; set; } = default!;

		[field: SerializeField]
		private Slider Slider { get; set; } = default!;

		[field: SerializeField]
		private UnityEvent<float> ValueChange { get; set; } = default!;

		public float Value { get => Slider.value; set => Slider.value = value; }

		public void InputChange(string value)
		{
			if (!float.TryParse(value, out var floatValue))
			{
				floatValue = 0;
			}

			var clampedValue = Mathf.Clamp(floatValue, Slider.minValue, Slider.maxValue);
			Slider.SetValueWithoutNotify(clampedValue);
			SliderChange(clampedValue);
		}

		public void SliderChange(float value)
		{
			SliderChange(value, true);
		}

		private void SliderChange(float value, bool notify)
		{
			InputField.SetTextWithoutNotify(
				Slider.wholeNumbers ? value.Round(0).ToString("0") : value.ToString("0.00"));

			if (notify)
			{
				ValueChange.Invoke(Value);
			}
		}

		public void SetValueWithoutNotify(float value)
		{
			Slider.SetValueWithoutNotify(value);
			SliderChange(value, false);
		}
	}
}
