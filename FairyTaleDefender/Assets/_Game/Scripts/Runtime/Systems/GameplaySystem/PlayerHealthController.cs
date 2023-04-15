using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.HealthSystem;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem
{
	[AddComponentMenu(Constants.MenuNames.GameplaySystem + "/" + nameof(PlayerHealthController))]
	public class PlayerHealthController : MonoBehaviour, IAmDamageable
	{
		[field: Header("References")]
		[field: SerializeField]
		public Health Health { get; set; } = default!;

		[field: SerializeField]
		public LevelRuntimeAnchorSO LevelRuntimeAnchor { get; private set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private IntEventChannelSO EnemyDamagesPlayerEventChannel { get; set; } = default!;

		[field: SerializeField]
		public VoidEventChannelSO SceneReadyEventChannel { get; private set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		private VoidEventChannelSO GameOverEventChannel { get; set; } = default!;

		private void OnEnable()
		{
			EnemyDamagesPlayerEventChannel.Raised += EnemyDamagesPlayer;
			Health.Dead += GameOver;
			SceneReadyEventChannel.Raised += PrepareHealth;
		}

		private void OnDisable()
		{
			EnemyDamagesPlayerEventChannel.Raised -= EnemyDamagesPlayer;
			Health.Dead -= GameOver;
			SceneReadyEventChannel.Raised -= PrepareHealth;
		}

		private void PrepareHealth()
		{
			Health.Initialize(LevelRuntimeAnchor.ItemSafe.PlayerStartResources.Health,
				LevelRuntimeAnchor.ItemSafe.PlayerStartResources.Health);
		}

		private void GameOver()
		{
			GameOverEventChannel.Raise();
		}

		private void EnemyDamagesPlayer(int damage)
		{
			Health.TakeDamage(damage);
		}
	}
}
