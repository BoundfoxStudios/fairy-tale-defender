using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.HealthSystem;
using TMPro;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem.UI
{
	[AddComponentMenu(Constants.MenuNames.GameplaySystem + "/" + nameof(PlayerHealth))]
	public class PlayerHealth : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private Health Health { get; set; } = default!;

		[field: SerializeField]
		private TextMeshProUGUI HealthText { get; set; } = default!;

		private void OnEnable()
		{
			Health.Change += HealthChange;
		}

		private void OnDisable()
		{
			Health.Change -= HealthChange;
		}

		private void HealthChange(int current, int change)
		{
			HealthText.text = current.ToString();
		}
	}
}
