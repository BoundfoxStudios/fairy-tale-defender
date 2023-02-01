using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.SceneManagement.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.SceneManagement
{
	[AddComponentMenu(Constants.MenuNames.SceneManagement + "/" + nameof(SceneLoadRequester))]
	public class SceneLoadRequester : MonoBehaviour
	{
		[field: Header("Settings")]
		[field: SerializeField]
		private SceneSO SceneToLoad { get; set; } = default!;

		[field: SerializeField]
		private bool ShowLoadingScreen { get; set; }

		[field: Header("Broadcasting on")]
		[field: SerializeField]
		private LoadSceneEventChannelSO LoadSceneEventChannel { get; set; } = default!;

		public void LoadScene()
		{
			LoadSceneEventChannel.Raise(new()
			{
				Scene = SceneToLoad,
				ShowLoadingScreen = ShowLoadingScreen
			});
		}
	}
}
