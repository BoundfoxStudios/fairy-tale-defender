using System;
using System.Collections.Generic;
using BoundfoxStudios.FairyTaleDefender.Infrastructure;

namespace BoundfoxStudios.FairyTaleDefender.Systems.SaveGameSystem
{
	/// <summary>
	/// This is the actual save game that will be written to a file.
	/// </summary>
	[Serializable]
	public class SaveGameData
	{
		/// <summary>
		/// Identifies the last played level.
		/// </summary>
		public ScriptableObjectIdentity? LastLevel;

		/// <summary>
		/// List of all unlocked levels.
		/// </summary>
		public List<ScriptableObjectIdentity> UnlockedLevels = new();
	}
}
