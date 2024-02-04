using System;

namespace BoundfoxStudios.FairyTaleDefender.Systems.SaveGameSystem
{
	/// <summary>
	/// Meta Information about a save game.
	/// </summary>
	[Serializable]
	public class SaveGameMeta
	{
		public string Name = string.Empty;
		public DateTime LastPlayedDate;

		public string Directory { get; set; }
	}
}
