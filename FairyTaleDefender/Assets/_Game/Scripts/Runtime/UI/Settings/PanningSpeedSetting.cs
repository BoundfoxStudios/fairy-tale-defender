using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.UI.Utility;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BoundfoxStudios.FairyTaleDefender.UI.Settings
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(PanningSpeedSetting))]
	public class PanningSpeedSetting : SettingBase
	{
		[field: SerializeField]
		private SliderWithValue Slider { get; set; } = default!;

		public override void ResetSettings(SettingsSO mutableSettings)
		{
			base.ResetSettings(mutableSettings);

			// Note: We remap from PanSpeed's [Range] attribute to 0 - 100
			Slider.SetValueWithoutNotify(math.remap(0, 15, 0, 100, mutableSettings.Camera.PanSpeed));
		}

		public void SliderChange(float value)
		{
			MutableSettings.Camera.PanSpeed = math.remap(0, 100, 0, 15, value);
			OnSettingsChange();
		}
	}
}
