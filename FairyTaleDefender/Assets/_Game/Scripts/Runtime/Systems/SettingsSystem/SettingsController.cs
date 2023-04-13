using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

namespace BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem
{
	/// <summary>
	/// Responsible for applying game settings to their systems.
	/// </summary>
	[AddComponentMenu(Constants.MenuNames.MenuName + "/" + nameof(SettingsController))]
	public class SettingsController : MonoBehaviour
	{
		[field: SerializeField]
		private SettingsSO Settings { get; set; } = default!;

		[field: SerializeField]
		private AudioMixer MainMixer { get; set; } = default!;

		[field: Header("Listening on")]
		[field: SerializeField]
		private VoidEventChannelSO GameSettingsChangedEventChannel { get; set; } = default!;

		private void Awake()
		{
			SetStartLocale();
			SetStartResolution();
			ApplySettings();
		}

		private void OnEnable()
		{
			GameSettingsChangedEventChannel.Raised += ApplySettings;
		}

		private void OnDisable()
		{
			GameSettingsChangedEventChannel.Raised -= ApplySettings;
		}

		private void SetStartLocale()
		{
			if (Settings.Localization.Locale == default)
			{
				Settings.Localization.Locale = LocalizationSettings.SelectedLocale.Identifier;
			}
		}

		private void SetStartResolution()
		{
			if (Settings.Graphic.ScreenWidth == 0 || Settings.Graphic.ScreenHeight == 0)
			{
				Settings.Graphic.ScreenWidth = Screen.width;
				Settings.Graphic.ScreenHeight = Screen.height;
			}
		}

		private void ApplySettings()
		{
			ApplyAudioSettings();
			ApplyScreenSettings();
			ApplyGraphicSettings();
			ApplyLocalizationSettings();
		}

		private void ApplyAudioSettings()
		{
			MainMixer.SetFloat("MasterVolume", GetNormalizedToMixerVolume(Settings.Audio.MasterVolume));
			MainMixer.SetFloat("MusicVolume", GetNormalizedToMixerVolume(Settings.Audio.MusicVolume));
			MainMixer.SetFloat("EffectsVolume", GetNormalizedToMixerVolume(Settings.Audio.EffectsVolume));
			MainMixer.SetFloat("UIVolume", GetNormalizedToMixerVolume(Settings.Audio.UIVolume));
		}

		private float GetNormalizedToMixerVolume(float value)
		{
			return (value - 1) * 80;
		}

		private void ApplyScreenSettings()
		{
			Screen.SetResolution(
				Settings.Graphic.ScreenWidth,
				Settings.Graphic.ScreenHeight,
				Settings.Graphic.IsFullscreen
			);
		}

		private void ApplyGraphicSettings()
		{
			QualitySettings.SetQualityLevel((int)Settings.Graphic.GraphicLevel);
		}

		private void ApplyLocalizationSettings()
		{
			LocalizationSettings.SelectedLocale =
				LocalizationSettings.AvailableLocales.GetLocale(Settings.Localization.Locale);
		}
	}
}
