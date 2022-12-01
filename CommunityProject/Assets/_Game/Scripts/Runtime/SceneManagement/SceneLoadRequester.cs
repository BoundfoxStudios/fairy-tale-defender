using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.SceneManagement.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.SceneManagement
{
	[AddComponentMenu(Constants.MenuNames.SceneManagement + "/" + nameof(SceneLoadRequester))]
	public class SceneLoadRequester : MonoBehaviour
	{
		[Header("Settings")]
		[SerializeField]
		private SceneSO SceneToLoad;

		[SerializeField]
		private bool ShowLoadingScreen;

		[Header("Broadcasting on")]
		[SerializeField]
		private LoadSceneEventChannelSO LoadSceneEventChannel;

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
