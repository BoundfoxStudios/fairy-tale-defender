using System;
using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.ObjectiveSystem.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.ObjectiveSystem
{
	[AddComponentMenu(Constants.MenuNames.Objectives + "/" + nameof(ObjectiveManager))]
	public class ObjectiveManager : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		public LevelRuntimeAnchorSO LevelRuntimeAnchor { get; private set; } = default!;

		[field: SerializeField]
		public AllLevelPacksSO AllLevelPacks { get; private set; } = default!;

		[field: SerializeField]
		public MenuSO MainMenuScene { get; private set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		public VoidEventChannelSO GameplayStartedEventChannel { get; private set; } = default!;

		[field: SerializeField]
		public VoidEventChannelSO ObjectiveCompletedEventChannel { get; private set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		public VoidEventChannelSO AllObjectivesCompletedEventChannel { get; private set; } = default!;

		[field: SerializeField]
		public LoadSceneEventChannelSO LoadSceneEventChannel { get; private set; } = default!;

		public LevelObjectivesSO CurrentLevelObjectives { get; private set; } = default!;

		private void OnEnable()
		{
			ObjectiveCompletedEventChannel.Raised += ObjectiveCompleted;
			GameplayStartedEventChannel.Raised += GameplayStarted;
		}

		private void OnDisable()
		{
			ObjectiveCompletedEventChannel.Raised -= ObjectiveCompleted;
			GameplayStartedEventChannel.Raised += GameplayStarted;
		}

		private void GameplayStarted()
		{
			CurrentLevelObjectives = LevelRuntimeAnchor.ItemSafe.Objectives;
		}

		private void ObjectiveCompleted()
		{
			if (!AllObjectivesCompleted())
			{
				return;
			}

			AllObjectivesCompletedEventChannel.Raise();

			LoadNextScene();
		}

		private void LoadNextScene()
		{
			var allLevel = AllLevelPacks.LevelPacks.SelectMany(levelPack => levelPack.Levels).ToArray();
			var currentSceneIndex = Array.IndexOf(allLevel, LevelRuntimeAnchor.ItemSafe);

			if (currentSceneIndex == allLevel.Length - 1)
			{
				LoadSceneEventChannel.Raise(new() { Scene = MainMenuScene, ShowLoadingScreen = true });
				return;
			}

			var nextScene = allLevel[currentSceneIndex + 1];
			LoadSceneEventChannel.Raise(new() { Scene = nextScene, ShowLoadingScreen = true });
		}

		private bool AllObjectivesCompleted()
		{
			return CurrentLevelObjectives.Objectives.All(p => p.IsCompleted);
		}
	}
}
