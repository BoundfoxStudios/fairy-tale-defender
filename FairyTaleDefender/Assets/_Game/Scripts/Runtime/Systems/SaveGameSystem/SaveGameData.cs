using System;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.ScriptableObjects;

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
		public IdentifiableSO? LastLevel;
	}
}
