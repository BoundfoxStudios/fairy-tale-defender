using System;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.FileManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;

namespace BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects
{
	/// <summary>
	/// Holding information about game settings.
	/// </summary>
	[CreateAssetMenu(fileName = "GameSettings", menuName = Constants.MenuNames.MenuName + "/GameSettings")]
	public class SettingsSO : ScriptableObject
	{
		private GameSettings? _gameSettings;

		public AudioConfig Audio => _gameSettings.EnsureOrThrow().Audio;
		public GraphicConfig Graphic => _gameSettings.EnsureOrThrow().Graphic;
		public LocalizationConfig Localization => _gameSettings.EnsureOrThrow().Localization;
		public CameraConfig Camera => _gameSettings.EnsureOrThrow().Camera;

		private JsonFileManager _jsonFileManager = default!;
		private readonly string _jsonFileName = "config.json";

		private void OnEnable()
		{
			_gameSettings = new();
			_jsonFileManager = new();
		}

		public async UniTask SaveAsync()
		{
			await _jsonFileManager.WriteAsync(_jsonFileName, _gameSettings.EnsureOrThrow());
		}

		public async UniTask LoadAsync()
		{
			var fileExists = await _jsonFileManager.ExistsAsync(_jsonFileName);

			if (!fileExists)
				return;

			_gameSettings = await _jsonFileManager.ReadAsync<GameSettings>(_jsonFileName);
		}

		[Serializable]
		public class GameSettings
		{
			public AudioConfig Audio = new();
			public GraphicConfig Graphic = new();
			public LocalizationConfig Localization = new();
			public CameraConfig Camera = new();
		}

		[Serializable]
		public class AudioConfig
		{
			[Range(0f, 1f)]
			public float MasterVolume = 1f;
			[Range(0f, 1f)]
			public float EffectsVolume = 1f;
			[Range(0f, 1f)]
			public float MusicVolume = 1f;
			[Range(0f, 1f)]
			public float UIVolume = 1f;
		}

		[Serializable]
		public class GraphicConfig
		{
			public int ScreenWidth;
			public int ScreenHeight;
			public bool IsFullscreen = true;
			public int GraphicLevel = 1;
		}

		[Serializable]
		public class LocalizationConfig
		{
			public LocaleIdentifier Locale;
		}

		[Serializable]
		public class CameraConfig
		{
			public bool EnableEdgePanning = true;
			public bool EnableKeyboardPanning = true;

			[Range(0, 15)]
			public float PanSpeed = 7.5f;
		}
	}
}
