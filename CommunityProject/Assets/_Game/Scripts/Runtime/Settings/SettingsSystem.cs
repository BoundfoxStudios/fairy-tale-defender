using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Settings.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace BoundfoxStudios.CommunityProject.Settings
{
	/// <summary>
	/// Responsible for applying game settings to their systems.
	/// </summary>
	[AddComponentMenu(Constants.MenuNames.MenuName + "/" + nameof(SettingsSystem))]
	public class SettingsSystem : MonoBehaviour
	{
		[SerializeField]
		private SettingsSO Settings;
		[SerializeField]
		private AudioMixer MainMixer;

		[Header("Listening on")]
		[SerializeField]
		private VoidEventChannelSO GameSettingsChangedEventChannel;

		private void Awake()
		{
			ApplySettings();
		}

		private void OnEnable()
		{
			GameSettingsChangedEventChannel.Raised += ApplySettings;
		}

		private async UniTask OnDisable()
		{
			GameSettingsChangedEventChannel.Raised -= ApplySettings;

			await Settings.SaveAsync();
		}

		private void ApplySettings()
		{
			MainMixer.SetFloat("MasterVolume", GetNormalizedToMixerVolume(Settings.Audio.MasterVolume));
			MainMixer.SetFloat("MusicVolume", GetNormalizedToMixerVolume(Settings.Audio.MusicVolume));
			MainMixer.SetFloat("EffectsVolume", GetNormalizedToMixerVolume(Settings.Audio.SFXVolume));
			MainMixer.SetFloat("UIVolume", GetNormalizedToMixerVolume(Settings.Audio.UIVolume));


			var resolutionIndex = Settings.Graphic.ResolutionIndex;

			if (resolutionIndex < 0 || resolutionIndex > Screen.resolutions.Length)
			{
				resolutionIndex = Screen.resolutions.Length - 1;
				Settings.Graphic.ResolutionIndex = resolutionIndex;
			}

			Screen.SetResolution(
						Screen.resolutions[resolutionIndex].width,
						Screen.resolutions[resolutionIndex].height,
						Settings.Graphic.IsFullscreen
						);

			QualitySettings.SetQualityLevel(Settings.Graphic.GraphicLevel);
		}

		private float GetNormalizedToMixerVolume(float value)
		{
			return (value - 1) * 80;
		}
	}
}
