using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.FairyTaleDefender.Systems.BuildSystem.UI
{
	public abstract class BuildButton<T> : MonoBehaviour
		where T : IAmBuildable, IHaveAPrice
	{
		[field: Header("References")]
		[field: SerializeField]
		protected T Buildable { get; set; } = default!;

		[field: SerializeField]
		public Button Button { get; private set; } = default!;

		[field: SerializeField]
		private TMP_Text CoinDisplay { get; set; } = default!;

		[field: SerializeField]
		public PlayerCoinsController PlayerCoinsController { get; private set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		public IntDeltaEventChannelSO CoinsChangeEventChannel { get; private set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		protected BuildableEventChannelSO EnterBuildModeEventChannel { get; set; } = default!;

		private void OnEnable()
		{
			CoinsChangeEventChannel.Raised += CoinsChange;

			CoinDisplay.text = Buildable.Price.ToString();
		}

		private void OnDisable()
		{
			CoinsChangeEventChannel.Raised -= CoinsChange;
		}

		private void CoinsChange(IntDeltaEventChannelSO.EventArgs args)
		{
			SetStateDependingOnCoins(args.Value);
		}

		private void SetStateDependingOnCoins(int coins)
		{
			Button.interactable = PlayerCoinsController.CanAfford(Buildable);
		}

		public void EnterBuildMode()
		{
			EnterBuildModeEventChannel.Raise(new() { Buildable = Buildable });
		}
	}
}
