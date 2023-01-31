using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Infrastructure;
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

		private void OnValidate()
		{
			Guard.AgainstNull(() => SceneToLoad, this);
			Guard.AgainstNull(() => LoadSceneEventChannel, this);
		}

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
