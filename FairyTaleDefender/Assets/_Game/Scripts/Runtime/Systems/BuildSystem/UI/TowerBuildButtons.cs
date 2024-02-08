using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Buildings.Towers.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeSets;
using BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem;
using BoundfoxStudios.FairyTaleDefender.Systems.TooltipSystem;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.BuildSystem.UI
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(TowerBuildButtons))]
	public class TowerBuildButtons : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private GameObject BuildButtonPrefab { get; set; } = default!;

		[field: SerializeField]
		private PlayerCoinsController PlayerCoinsController { get; set; } = default!;

		[field: SerializeField]
		private BuildableTowerRuntimeSetSO AvailableTowers { get; set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private VoidEventChannelSO AvailableTowersChangedEventChannel { get; set; } = default!;

		private void OnEnable()
		{
			AvailableTowersChangedEventChannel.Raised += AvailableTowersChanged;
			CreateButtons();
		}

		private void OnDisable()
		{
			AvailableTowersChangedEventChannel.Raised -= AvailableTowersChanged;
		}

		private void AvailableTowersChanged()
		{
			CreateButtons();
		}

		private void CreateButtons()
		{
			transform.ClearChildren();

			foreach (var tower in AvailableTowers.Items)
			{
				CreateButton(tower);
			}
		}

		private void CreateButton(BuildableTowerSO tower)
		{
			var newButtonGameObject = Instantiate(BuildButtonPrefab, transform);
			var button = newButtonGameObject.GetComponent<TowerBuildButton>();
			var tooltip = newButtonGameObject.GetComponent<BuildTowerTooltip>();

			button.Init(tower, PlayerCoinsController);
			tooltip.Init(tower);
		}
	}
}
