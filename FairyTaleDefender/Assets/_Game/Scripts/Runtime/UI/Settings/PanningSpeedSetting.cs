using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.UI.Utility;
using Unity.Mathematics;
using UnityEngine;

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

			Slider.SetValueWithoutNotify(math.remap(Constants.Settings.Panning.Start, Constants.Settings.Panning.End, 0, 100,
				mutableSettings.Camera.PanSpeed));
		}

		public void SliderChange(float value)
		{
			MutableSettings.Camera.PanSpeed =
				math.remap(0, 100, Constants.Settings.Panning.Start, Constants.Settings.Panning.End, value);
			OnSettingsChange();
		}
	}
}
