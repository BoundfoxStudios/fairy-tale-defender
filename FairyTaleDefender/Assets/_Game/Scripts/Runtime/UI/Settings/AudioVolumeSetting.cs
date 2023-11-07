using System;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.UI.Utility;
using Unity.Mathematics;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI.Settings
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(AudioVolumeSetting))]
	public class AudioVolumeSetting : SettingBase
	{
		public enum AudioMixer
		{
			Master,
			Effects,
			Music,
			UI
		}

		[field: SerializeField]
		private SliderWithValue Slider { get; set; } = default!;

		[field: SerializeField]
		private AudioMixer Mixer { get; set; } = AudioMixer.Master;

		public override void ResetSettings(SettingsSO mutableSettings)
		{
			base.ResetSettings(mutableSettings);

			// Note: We remap from AudioConfig's [Range] attribute to 0 - 100
			Slider.SetValueWithoutNotify(math.remap(0, 1, 0, 100, GetValue(Mixer)));
		}

		public void SliderChange(float value)
		{
			SetValue(Mixer, value);
			OnSettingsChange();
		}

		private float GetValue(AudioMixer audioMixer) => audioMixer switch
		{
			AudioMixer.Master => MutableSettings.Audio.MasterVolume,
			AudioMixer.Effects => MutableSettings.Audio.EffectsVolume,
			AudioMixer.Music => MutableSettings.Audio.MusicVolume,
			AudioMixer.UI => MutableSettings.Audio.UIVolume,
			_ => throw new ArgumentOutOfRangeException(nameof(audioMixer), audioMixer, null)
		};

		private void SetValue(AudioMixer audioMixer, float value)
		{
			var remappedValue = math.remap(0, 100, 0, 1, value);

			switch (audioMixer)
			{
				case AudioMixer.Master:
					MutableSettings.Audio.MasterVolume = remappedValue;
					break;
				case AudioMixer.Effects:
					MutableSettings.Audio.EffectsVolume = remappedValue;
					break;
				case AudioMixer.Music:
					MutableSettings.Audio.MusicVolume = remappedValue;
					break;
				case AudioMixer.UI:
					MutableSettings.Audio.UIVolume = remappedValue;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(audioMixer), audioMixer, null);
			}
		}
	}
}
