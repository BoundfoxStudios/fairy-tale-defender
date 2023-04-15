using System;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace BoundfoxStudios.FairyTaleDefender
{
	[Serializable]
	public class SteamStartupLocaleSelector : IStartupLocaleSelector
	{
		[field: SerializeField]
		public SteamRuntimeAnchorSO SteamRuntimeAnchor { get; private set; } = default!;

		[field: SerializeField]
		public SettingsSO Settings { get; private set; } = default!;

		public Locale? GetStartupLocale(ILocalesProvider availableLocales)
		{
			var gameLanguage = SteamRuntimeAnchor.ItemSafe.SteamApps.GetCurrentGameLanguage();

			Debug.Log($"Game language: {gameLanguage}");

			if (gameLanguage is null)
			{
				return null;
			}

			var locale = availableLocales.GetLocale(gameLanguage);

			if (locale is null)
			{
				return null;
			}

			Settings.Localization.LocaleSetViaSteam = true;
			return locale;
		}
	}
}
