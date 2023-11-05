using BoundfoxStudios.FairyTaleDefender.Common;
using TMPro;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem.UI
{
	[AddComponentMenu(Constants.MenuNames.GameplaySystem + "/" + nameof(StatisticDisplay))]
	public class StatisticDisplay : MonoBehaviour
	{
		[field: SerializeField]
		private TextMeshProUGUI KeyText { get; set; } = default!;

		[field: SerializeField]
		private TextMeshProUGUI ValueText { get; set; } = default!;

		public void SetTexts(string key, string value)
		{
			KeyText.text = key;
			ValueText.text = value;
		}
	}
}
