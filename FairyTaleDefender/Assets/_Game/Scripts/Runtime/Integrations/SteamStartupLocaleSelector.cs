using System;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
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

		public Locale? GetStartupLocale(ILocalesProvider availableLocales)
		{
			var gameLanguage = SteamRuntimeAnchor.ItemSafe.SteamApps.GetCurrentGameLanguage();

			Debug.Log($"Game language: {gameLanguage}");

			return gameLanguage is null ? null : availableLocales.GetLocale(gameLanguage);
		}
	}
}
