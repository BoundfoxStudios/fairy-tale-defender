using BoundfoxStudios.CommunityProject.Entities.Characters.Enemies.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Infrastructure.RuntimeAnchors.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Systems.GameplaySystem
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
		public EnemyEventChannelSO EnemyDestroyedByPlayerEventChannel { get; private set; } = default!;

		private int _coins;

		private int Coins
		{
			get => _coins;
			set
			{
				_coins = value;
				CoinsText.text = _coins.ToString();
			}
		}

		private void OnEnable()
		{
			SceneReadyEventChannel.Raised += PrepareResources;
			EnemyDestroyedByPlayerEventChannel.Raised += EnemyDestroyedByPlayer;
		}

		private void OnDisable()
		{
			SceneReadyEventChannel.Raised -= PrepareResources;
			EnemyDestroyedByPlayerEventChannel.Raised -= EnemyDestroyedByPlayer;
		}

		private void EnemyDestroyedByPlayer(EnemySO enemy)
		{
			Coins += enemy.CoinsOnKill;
		}

		private void PrepareResources()
		{
			Coins = LevelRuntimeAnchor.ItemSafe.PlayerStartResources.Coins;
		}
	}
}
