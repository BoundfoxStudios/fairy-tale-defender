using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
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

		[field: Header("Listening Channels")]
		[field: SerializeField]
		public VoidEventChannelSO GameplayStartedEventChannel { get; private set; } = default!;

		[field: SerializeField]
		public VoidEventChannelSO ObjectiveCompletedEventChannel { get; private set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		public VoidEventChannelSO AllObjectivesCompletedEventChannel { get; private set; } = default!;

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
		}

		private bool AllObjectivesCompleted()
		{
			return CurrentLevelObjectives.Objectives.All(p => p.IsCompleted);
		}
	}
}
