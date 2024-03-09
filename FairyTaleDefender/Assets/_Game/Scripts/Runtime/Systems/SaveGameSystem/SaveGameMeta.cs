using System;
using System.IO;
using BoundfoxStudios.FairyTaleDefender.Common;

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
		public string Hash = string.Empty;

		public string Directory { get; set; } = string.Empty;

		public string MetaFilePath => Path.Combine(Directory, Constants.SaveGames.MetaFileName);
		public string DataFilePath => Path.Combine(Directory, Constants.SaveGames.SaveGameFileName);
	}
}
