using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Characters.Enemies.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.BuildSystem;
using TMPro;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem
{
	[AddComponentMenu(Constants.MenuNames.GameplaySystem + "/" + nameof(PlayerCoinsController))]
	public class PlayerCoinsController : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		public LevelRuntimeAnchorSO LevelRuntimeAnchor { get; private set; } = default!;

		[field: SerializeField]
		public TextMeshProUGUI CoinsText { get; private set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		public VoidEventChannelSO SceneReadyEventChannel { get; private set; } = default!;

		[field: SerializeField]
		public EnemySOEventChannelSO EnemyDestroyedByPlayerEventChannel { get; private set; } = default!;

		[field: SerializeField]
		public BuildableEventChannelSO BuiltEventChannel { get; private set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		public IntDeltaEventChannelSO CoinsChangeEventChannel { get; private set; } = default!;

		private int _coins;

		private int Coins
		{
			get => _coins;
			set
			{
				var delta = value - _coins;

				_coins = value;

				CoinsText.text = _coins.ToString();
				CoinsChangeEventChannel.Raise(new() { Value = _coins, Delta = delta });
			}
		}

		private void OnEnable()
		{
			SceneReadyEventChannel.Raised += PrepareResources;
			EnemyDestroyedByPlayerEventChannel.Raised += EnemyDestroyedByPlayer;
			BuiltEventChannel.Raised += Built;
		}

		private void OnDisable()
		{
			SceneReadyEventChannel.Raised -= PrepareResources;
			EnemyDestroyedByPlayerEventChannel.Raised -= EnemyDestroyedByPlayer;
			BuiltEventChannel.Raised -= Built;
		}

		private void EnemyDestroyedByPlayer(EnemySO enemy)
		{
			Coins += enemy.CoinsOnKill;
		}

		private void PrepareResources()
		{
			Coins = LevelRuntimeAnchor.ItemSafe.PlayerStartResources.Coins;
		}

		private void Built(BuildableEventChannelSO.EventArgs args)
		{
			var buildable = args.Buildable;

			if (buildable is IHaveAPrice buildableWithPrice)
			{
				Coins -= buildableWithPrice.Price;
			}
		}

		public bool CanAfford(IHaveAPrice tower)
		{
			return Coins >= tower.Price;
		}
	}
}
