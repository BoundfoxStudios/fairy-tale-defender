using System;
using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;
using UnityEngine.Localization;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects
{
	/// <summary>
	/// Contains a collection of scenes.
	/// e.g. LevelPack: LittleRedRidingHood contains 4 Levels
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.SceneManagement + "/Level Pack")]
	public class LevelPackSO : ScriptableObject
	{
		[field: SerializeField]
		public LocalizedString Name { get; private set; } = default!;

		[field: SerializeField]
		public LevelSO[] Levels { get; set; } = default!;

		[field: NonSerialized]
		public LevelPackSO? NextLevelPack { get; set; }

		[field: NonSerialized]
		public LevelPackSO? PreviousLevelPack { get; set; }
	}
}
