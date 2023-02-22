using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Systems.HealthSystem;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Systems.GameplaySystem
{
	[AddComponentMenu(Constants.MenuNames.GameplaySystem + "/" + nameof(PlayerController))]
	public class PlayerController : MonoBehaviour, IAmDamageable
	{
		[field: Header("References")]
		[field: SerializeField]
		public Health Health { get; set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private IntEventChannelSO EnemyDamagesPlayerEventChannel { get; set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		private VoidEventChannelSO GameOverEventChannel { get; set; } = default!;

		private void Awake()
		{
			// TODO: Where should we get this information from?
			Health.Initialize(10, 10);
		}

		private void OnEnable()
		{
			EnemyDamagesPlayerEventChannel.Raised += EnemyDamagesPlayer;
			Health.Dead += GameOver;
		}

		private void OnDisable()
		{
			EnemyDamagesPlayerEventChannel.Raised -= EnemyDamagesPlayer;
			Health.Dead -= GameOver;
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
