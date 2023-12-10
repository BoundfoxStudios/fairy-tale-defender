using System;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.ObjectiveSystem.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.SpawnSystem.Waves.ScriptableObjects;
using UnityEngine;
using UnityEngine.Localization;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects
{
	/// <summary>
	/// Describes a playable level.
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.SceneManagement + "/Level")]
	public class LevelSO : SceneSO
	{
		[field: SerializeField]
		public LocalizedString Name { get; private set; } = default!;

		[field: SerializeField]
		public WavesSO Waves { get; private set; } = default!;

		[field: SerializeField]
		public LevelObjectivesSO Objectives { get; private set; } = default!;

		[field: SerializeField]
		public PlayerResources PlayerStartResources { get; private set; } = default!;

		[Serializable]
		public class PlayerResources
		{
			[field: SerializeField]
			[field: Range(10, 50)]
			public int Health { get; private set; } = 10;

			[field: SerializeField]
			[field: Range(50, 200)]
			public int Coins { get; private set; } = 100;
		}
	}
}
