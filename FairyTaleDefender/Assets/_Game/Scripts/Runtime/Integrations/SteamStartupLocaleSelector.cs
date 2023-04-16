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
			var steam = SteamRuntimeAnchor.Item;

			if (steam is null)
			{
				return null;
			}

			var gameLanguage = steam.SteamApps.GetCurrentGameLanguage();
			
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
