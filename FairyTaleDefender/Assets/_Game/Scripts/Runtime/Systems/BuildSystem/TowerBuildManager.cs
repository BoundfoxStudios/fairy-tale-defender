using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeSets;
using BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem;
using BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.BuildSystem
{
	public class TowerBuildManager : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		public InputReaderSO InputReader { get; private set; } = default!;

		[field: SerializeField]
		public BuildableTowerRuntimeSetSO AvailableTowers { get; private set; } = default!;

		[field: SerializeField]
		public PlayerCoinsController PlayerCoinsController { get; private set; } = default!;


		[field: SerializeField]
		public LevelRuntimeAnchorSO LevelRuntimeAnchor { get; private set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		public BuildableEventChannelSO EnterBuildModeEventChannel { get; private set; } = default!;

		private void OnEnable()
		{
			InputReader.GameplayActions.BuildTower += EnterBuildMode;

			foreach (var startTower in LevelRuntimeAnchor.ItemSafe.StartTowers)
			{
				AvailableTowers.Add(startTower);
			}
		}

		private void OnDisable()
		{
			InputReader.GameplayActions.BuildTower -= EnterBuildMode;
			AvailableTowers.Clear();
		}

		private void EnterBuildMode(int index)
		{
			if (index >= AvailableTowers.Items.Count)
			{
				return;
			}

			var tower = AvailableTowers.Items[index];

			if (!PlayerCoinsController.CanAfford(tower))
			{
				return;
			}
			EnterBuildModeEventChannel.Raise(new BuildableEventChannelSO.EventArgs { Buildable = tower });
		}
	}
}
