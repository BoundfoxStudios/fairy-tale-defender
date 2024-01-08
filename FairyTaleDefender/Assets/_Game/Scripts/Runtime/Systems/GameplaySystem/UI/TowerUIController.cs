using System.Collections.Generic;
using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.UI.Utility;
using TMPro;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem.UI
{
	[AddComponentMenu(Constants.MenuNames.GameplaySystem + "/" + nameof(TowerUIController))]
	public class TowerUIController : MonoBehaviour
	{
		private enum StatisticType
		{
			Range,
			FireRate,
			Angle,
		}

		[field: Header("References")]
		[field: SerializeField]
		private GameObject TowerUIContainer { get; set; } = default!;

		[field: SerializeField]
		private GameObject StatisticsContainer { get; set; } = default!;

		[field: SerializeField]
		private StatisticDisplay StatisticsRowPrefab { get; set; } = default!;

		[field: SerializeField]
		private TextMeshProUGUI TowerNameText { get; set; } = default!;

		[field: SerializeField]
		private ToggleButtonGroup TargetTypeToggleButtonGroup { get; set; } = default!;

		[field: SerializeField]
		private List<TargetTypeSO> TargetTypes { get; set; } = default!;

		[field: Header("Listening Event Channels")]
		[field: SerializeField]
		public WeaponSelectedEventChannelSO WeaponSelectedEventChannel { get; private set; } = default!;

		private readonly List<StatisticType> _statisticOrder = new()
		{
			StatisticType.Range,
			StatisticType.FireRate,
			StatisticType.Angle,
		};

		private readonly Dictionary<StatisticType, StatisticDisplay> _statistics = new();

		private WeaponSelectedEventChannelSO.EventArgs? _currentSelection;

		private void Awake()
		{
			TowerUIContainer.SetActive(false);
		}

		private void OnEnable()
		{
			WeaponSelectedEventChannel.Raised += Show;
			TargetTypeToggleButtonGroup.IndexChanged += ChangeTargetType;
		}

		private void ChangeTargetType(int targetTypeIndex)
		{
			var targetType = _currentSelection?.Transform.GetComponentInChildren<ICanChangeTargetType>();

			Debug.Assert(targetType != null);

			targetType!.TargetType = TargetTypes[targetTypeIndex];
		}

		private void OnDisable()
		{
			_currentSelection = null;
			WeaponSelectedEventChannel.Raised -= Show;
			TargetTypeToggleButtonGroup.IndexChanged -= ChangeTargetType;
		}

		private void Show(WeaponSelectedEventChannelSO.EventArgs args)
		{
			_currentSelection = args;
			_statistics.Clear();
			StatisticsContainer.transform.ClearChildren();
			TowerUIContainer.SetActive(true);

			TowerNameText.text = args.Tower.Name.GetLocalizedString();
			var targetType = _currentSelection?.Transform.GetComponentInChildren<ICanChangeTargetType>();

			TargetTypeToggleButtonGroup.Index = TargetTypes.FindIndex(type => type == targetType!.TargetType);

			var parent = StatisticsContainer.transform;
			var definition = args.EffectiveWeaponDefinition.CalculateEffectiveWeaponDefinition(args.Transform.position);

			if (definition is EffectiveBallisticWeaponDefinition ballisticWeapon)
			{
				_statistics.TryAddLazy(StatisticType.Range,
					() => CreateStatisticDisplay("Range",
						$"{ballisticWeapon.MinimumRange.Format()}-{ballisticWeapon.MaximumRange.Format()}"));
			}

			_statistics.TryAddLazy(StatisticType.Range,
				() => CreateStatisticDisplay("Range", definition.Range.Format()));
			_statistics.TryAddLazy(StatisticType.FireRate,
				() => CreateStatisticDisplay("Fire Rate / s", (1 / definition.FireRateEverySeconds).Format(true)));
			_statistics.TryAddLazy(StatisticType.Angle,
				() => CreateStatisticDisplay("Angle", definition.AttackAngle.ToString()));

			var sortedStatistics = _statistics.OrderBy(kvp => _statisticOrder.IndexOf(kvp.Key)).Select(kvp => kvp.Value)
				.ToList();

			foreach (var statistic in sortedStatistics)
			{
				statistic.transform.SetParent(parent, false);
			}
		}

		private StatisticDisplay CreateStatisticDisplay(string key, string value)
		{
			var display = Instantiate(StatisticsRowPrefab);
			display.SetTexts(key, value);

			return display;
		}
	}
}
