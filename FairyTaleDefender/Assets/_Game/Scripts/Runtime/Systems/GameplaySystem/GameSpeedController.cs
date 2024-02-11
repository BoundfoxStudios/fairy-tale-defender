using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.UI.Utility;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem
{
	[AddComponentMenu(Constants.MenuNames.GameplaySystem + "/" + nameof(GameSpeedController))]
	public class GameSpeedController : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private ToggleButtonGroup GameSpeedToggleButtonGroup { get; set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		private FloatEventChannelSO SetGameSpeedEventChannel { get; set; } = default!;

		[field: Header("Listening Channels")]

		[field: SerializeField]
		private LevelFinishedEventChannelSO LevelFinishedEventChannel { get; set; } = default!;


		private void OnEnable()
		{
			LevelFinishedEventChannel.Raised += LevelFinished;
			GameSpeedToggleButtonGroup.IndexChanged += SetGameSpeed;
		}

		private void OnDisable()
		{
			LevelFinishedEventChannel.Raised -= LevelFinished;
			GameSpeedToggleButtonGroup.IndexChanged -= SetGameSpeed;
		}

		private void Awake()
		{
			SetSpeed(1);
		}

		private void Start()
		{
			GameSpeedToggleButtonGroup.Index = 0;
		}

		private void OnDestroy()
		{
			SetSpeed(1);
		}

		private void SetGameSpeed(int buttonIndex)
		{
			SetSpeed(buttonIndex + 1);
		}

		private void LevelFinished(LevelFinishedEventChannelSO.EventArgs _)
		{
			SetSpeed(1);
		}

		private void SetSpeed(float i)
		{
			Time.timeScale = i;
			SetGameSpeedEventChannel.Raise(i);
		}
	}
}
