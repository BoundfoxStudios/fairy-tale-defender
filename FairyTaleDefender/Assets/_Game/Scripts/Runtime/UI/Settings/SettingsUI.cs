using System;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace BoundfoxStudios.FairyTaleDefender.UI.Settings
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(SettingsUI))]
	public class SettingsUI : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private Button ApplyButton { get; set; } = default!;

		[field: SerializeField]
		private Button ResetButton { get; set; } = default!;

		[field: SerializeField]
		private SettingsSO Settings { get; set; } = default!;

		[field: Header("Broadcasting Channel")]
		[field: SerializeField]
		private VoidEventChannelSO SettingsChangedEventChannel { get; set; } = default!;

		private IAmASetting[] _settings = default!;

		private SettingsSO _mutableSettings = default!;

		private void Awake()
		{
			_settings = GetComponentsInChildren<IAmASetting>();
			AssignSettingsChangeEventListeners();
		}

		private void OnDestroy()
		{
			RemovesSettingChangeEventListeners();
		}

		private void Start()
		{
			ResetSettings();
		}

		private void AssignSettingsChangeEventListeners()
		{
			foreach (var setting in _settings)
			{
				setting.SettingsChange += SettingChange;
			}
		}

		private void RemovesSettingChangeEventListeners()
		{
			foreach (var setting in _settings)
			{
				setting.SettingsChange -= SettingChange;
			}
		}

		private void SettingChange()
		{
			ApplyButton.interactable = true;
			ResetButton.interactable = true;
		}

		public void ResetSettings()
		{
			ApplyButton.interactable = false;
			ResetButton.interactable = false;

			ResetSettingsAsync().Forget();
		}

		private async UniTaskVoid ResetSettingsAsync()
		{
			_mutableSettings = ScriptableObject.CreateInstance<SettingsSO>();
			await _mutableSettings.LoadAsync();

			foreach (var setting in _settings)
			{
				setting.ResetSettings(_mutableSettings);
			}
		}

		public void ApplySettings()
		{
			ApplySettingsAsync().Forget();
		}

		private async UniTaskVoid ApplySettingsAsync()
		{
			ApplyButton.interactable = false;
			ResetButton.interactable = false;

			await _mutableSettings.SaveAsync();
			await Settings.LoadAsync();

			SettingsChangedEventChannel.Raise();
		}
	}
}
