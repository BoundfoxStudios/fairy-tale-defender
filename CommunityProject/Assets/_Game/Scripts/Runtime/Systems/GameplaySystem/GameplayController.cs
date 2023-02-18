using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Systems.GameplaySystem
{
	[AddComponentMenu(Constants.MenuNames.CameraSystem + "/" + nameof(GameplayController))]
	public class GameplayController : MonoBehaviour
	{
		[field: Header("Listening Channels")]
		[field: SerializeField]
		private VoidEventChannelSO SceneReadyEventChannel { get; set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		private VoidEventChannelSO GameplayStartEventChannel { get; set; } = default!;

		private void OnEnable()
		{
			SceneReadyEventChannel.Raised += SceneReady;
		}

		private void OnDisable()
		{
			SceneReadyEventChannel.Raised -= SceneReady;
		}

		private void SceneReady()
		{
			GameplayStartEventChannel.Raise();
		}
	}
}
