using BoundfoxStudios.CommunityProject.Infrastructure.FileManagement;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Localization;

namespace BoundfoxStudios.CommunityProject.Settings.ScriptableObjects
{
	/// <summary>
	/// Holding information about game settings.
	/// </summary>
	[CreateAssetMenu(fileName = "GameSettings", menuName = Constants.MenuNames.MenuName + "/GameSettings")]
	public class SettingsSO : ScriptableObject
	{
		private GameSettings _gameSettings;

		public AudioConfig Audio => _gameSettings.Audio;
		public GraphicConfig Graphic => _gameSettings.Graphic;
		public LocalizationConfig Localization => _gameSettings.Localization;

		private JsonFileManager _jsonFileManager;
		private readonly string _jsonFileName = "config.json";

		private void OnEnable()
		{
			_jsonFileManager = new JsonFileManager();
		}

		public async UniTask SaveAsync()
		{
			await _jsonFileManager.WriteAsync<GameSettings>(_jsonFileName, _gameSettings);
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
			public AudioConfig Audio;
			public GraphicConfig Graphic;
			public LocalizationConfig Localization;
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
			public int ResolutionIndex = -1;
			public bool IsFullscreen = true;
			public int GraphicLevel = 1;
		}

		[Serializable]
		public class LocalizationConfig
		{
			public Locale Locale = default;
		}
	}
}
