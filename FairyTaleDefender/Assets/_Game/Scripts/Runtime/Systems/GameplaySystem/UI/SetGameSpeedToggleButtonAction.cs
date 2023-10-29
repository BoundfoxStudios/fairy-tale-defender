using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.UI.Utility;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem.UI
{
	public class SetGameSpeedToggleButtonAction : MonoBehaviour, IToggleButtonAction
	{
		[field: Header("Settings")]
		[field: SerializeField]
		private float GameSpeed { get; set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		private FloatEventChannelSO SetGameSpeedEventChannel { get; set; } = default!;

		public void ExecuteAction(int index) => SetGameSpeedEventChannel.Raise(GameSpeed);
	}
}
