using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Settings.ScriptableObjects;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

namespace BoundfoxStudios.CommunityProject.Settings
{
	/// <summary>
	/// Responsible for applying game settings to their systems.
	/// </summary>
	[AddComponentMenu(Constants.MenuNames.MenuName + "/" + nameof(SettingsSystem))]
	public class SettingsSystem : MonoBehaviour
	{
		[SerializeField]
		private SettingsSO Settings = default!;

		[SerializeField]
		private AudioMixer MainMixer = default!;

		[Header("Listening on")]
		[SerializeField]
		private VoidEventChannelSO GameSettingsChangedEventChannel = default!;

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
			QualitySettings.SetQualityLevel(Settings.Graphic.GraphicLevel);
		}

		private void ApplyLocalizationSettings()
		{
			LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(Settings.Localization.Locale);
		}
	}
}
