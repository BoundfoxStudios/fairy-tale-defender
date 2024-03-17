using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using Cysharp.Threading.Tasks;
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

		[field: SerializeField]
		private GameObject CursorEffects { get; set; } = default!;

		[field: Header("Listening on")]
		[field: SerializeField]
		private VoidEventChannelSO GameSettingsChangedEventChannel { get; set; } = default!;

		private void Awake()
		{
			var didChangeSettings = SetDefaultLocale();
			didChangeSettings = SetDefaultResolution() || didChangeSettings;
			ApplySettings();

			if (didChangeSettings)
			{
				Settings.SaveAsync().Forget();
			}
		}

		private void OnEnable()
		{
			GameSettingsChangedEventChannel.Raised += ApplySettings;
		}

		private void OnDisable()
		{
			GameSettingsChangedEventChannel.Raised -= ApplySettings;
		}

		private bool SetDefaultLocale()
		{
			if (Settings.Localization.Locale == default)
			{
				Settings.Localization.Locale = LocalizationSettings.SelectedLocale.Identifier;
				return true;
			}

			return false;
		}

		private bool SetDefaultResolution()
		{
			if (Settings.Graphic.ScreenWidth == 0 || Settings.Graphic.ScreenHeight == 0)
			{
				Settings.Graphic.ScreenWidth = Screen.currentResolution.width;
				Settings.Graphic.ScreenHeight = Screen.currentResolution.height;
				return true;
			}

			return false;
		}

		private void ApplySettings()
		{
			ApplyAudioSettings();
			ApplyScreenSettings();
			ApplyGraphicSettings();
			ApplyLocalizationSettings();
			ApplyCursorSettings();
		}

		private void ApplyCursorSettings()
		{
			CursorEffects.SetActive(Settings.Graphic.EnableCursorEffects);
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
			// Don't set the saved localization if it was already determined via Steam.
			if (Settings.Localization.LocaleSetViaSteam)
			{
				return;
			}

			LocalizationSettings.SelectedLocale =
				LocalizationSettings.AvailableLocales.GetLocale(Settings.Localization.Locale);
		}
	}
}
