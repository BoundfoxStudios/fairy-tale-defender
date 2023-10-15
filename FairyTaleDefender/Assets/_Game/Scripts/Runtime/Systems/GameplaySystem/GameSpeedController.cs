using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem
{
	[AddComponentMenu(Constants.MenuNames.GameplaySystem + "/" + nameof(GameSpeedController))]
	public class GameSpeedController : MonoBehaviour
	{
		[field: Header("Listening Channels")]
		[field: SerializeField]
		private FloatEventChannelSO SetGameSpeedEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO GameOverEventChannel { get; set; } = default!;


		private void OnEnable()
		{
			GameOverEventChannel.Raised += GameOver;
			SetGameSpeedEventChannel.Raised += SetGameSpeed;
		}

		private void OnDisable()
		{
			GameOverEventChannel.Raised -= GameOver;
			SetGameSpeedEventChannel.Raised -= SetGameSpeed;
		}

		private void Awake()
		{
			SetSpeed(1);
		}

		private void OnDestroy()
		{
			SetSpeed(1);
		}

		private void SetGameSpeed(float gameSpeed)
		{
			SetSpeed(gameSpeed);
		}

		private void GameOver()
		{
			SetSpeed(1);
		}

		private void SetSpeed(float i)
		{
			Time.timeScale = i;
		}
	}
}
