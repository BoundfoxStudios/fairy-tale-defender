using BoundfoxStudios.FairyTaleDefender.Common.Integrations.Steam;

namespace BoundfoxStudios.FairyTaleDefender.Integrations.Steam.Wrappers
{
	public class SteamAppsWrapper : SteamApps
	{
		public override string? GetCurrentGameLanguage()
		{
			return Steamworks.SteamApps.GetCurrentGameLanguage();
		}
	}
}
