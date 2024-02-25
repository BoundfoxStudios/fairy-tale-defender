using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.GameplaySystem
{
	[AddComponentMenu(Constants.MenuNames.GameplaySystem + "/" + nameof(PauseController))]
	public class PauseController : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		public InputReaderSO InputReader { get; private set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		public BoolEventChannelSO TogglePauseEventChannel { get; private set; } = default!;

		private bool _paused;

		private void OnEnable()
		{
			InputReader.GameplayActions.PauseGame += TogglePause;
			InputReader.PauseActions.ResumeGame += TogglePause;
		}

		private void OnDisable()
		{
			InputReader.GameplayActions.PauseGame -= TogglePause;
			InputReader.PauseActions.ResumeGame -= TogglePause;
		}

		public void TogglePause()
		{
			_paused = !_paused;
			TogglePauseEventChannel.Raise(_paused);
		}
	}
}
