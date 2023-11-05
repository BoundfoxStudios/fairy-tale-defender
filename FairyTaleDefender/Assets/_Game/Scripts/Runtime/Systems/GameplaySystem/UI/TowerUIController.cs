using System.Collections.Generic;
using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
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

		private void Awake()
		{
			TowerUIContainer.SetActive(false);
		}

		private void OnEnable()
		{
			WeaponSelectedEventChannel.Raised += Show;
		}

		private void OnDisable()
		{
			WeaponSelectedEventChannel.Raised -= Show;
		}

		private void Show(WeaponSelectedEventChannelSO.EventArgs args)
		{
			_statistics.Clear();
			StatisticsContainer.transform.ClearChildren();
			TowerUIContainer.SetActive(true);

			TowerNameText.text = args.Tower.Name.GetLocalizedString();

			var parent = StatisticsContainer.transform;
			var definition = args.EffectiveWeaponDefinition.CalculateEffectiveWeaponDefinition(args.Transform.position);

			if (definition is EffectiveBallisticWeaponDefinition ballisticWeapon)
			{
				_statistics.TryAddLazy(StatisticType.Range,
					() => CreateStatisticDisplay("Range",
						$"{ballisticWeapon.MinimumRange:0.##}-{ballisticWeapon.MaximumRange:0.##}"));
			}

			_statistics.TryAddLazy(StatisticType.Range,
				() => CreateStatisticDisplay("Range", definition.Range.ToString("0.##")));
			_statistics.TryAddLazy(StatisticType.FireRate,
				() => CreateStatisticDisplay("Fire Rate / s", (1 / definition.FireRateEverySeconds).ToString("0.00")));
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