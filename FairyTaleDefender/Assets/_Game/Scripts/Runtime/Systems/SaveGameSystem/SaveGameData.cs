using System;
using System.Collections.Generic;
using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Infrastructure;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.SaveGameSystem
{
	/// <summary>
	/// This is the actual save game that will be written to a file.
	/// </summary>
	[Serializable]
	public class SaveGameData : ISerializationCallbackReceiver
	{
		/// <summary>
		/// Identifies the last played level.
		/// </summary>
		public ScriptableObjectIdentity? LastLevel;

		/// <summary>
		/// List of all unlocked levels.
		/// </summary>
		public HashSet<ScriptableObjectIdentity> UnlockedLevels = new();

		[SerializeField]
		// ReSharper disable once InconsistentNaming
		private List<ScriptableObjectIdentity> _unlockedLevels = new();

		public void OnBeforeSerialize()
		{
			_unlockedLevels = UnlockedLevels.ToList();
		}

		public void OnAfterDeserialize()
		{
			UnlockedLevels = new(_unlockedLevels);
		}
	}
}
