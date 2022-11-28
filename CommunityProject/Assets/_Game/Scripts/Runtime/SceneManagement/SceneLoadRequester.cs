using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.SceneManagement
{
	[AddComponentMenu(Constants.MenuNames.SceneManagement + "/" + nameof(SceneLoadRequester))]
	public class SceneLoadRequester : MonoBehaviour
	{
		[SerializeField]
		private LoadSceneEventChannelSO.EventArgs Settings;

		[Header("Broadcasting on")]
		[SerializeField]
		private LoadSceneEventChannelSO LoadSceneEventChannel;

		public void LoadScene()
		{
			LoadSceneEventChannel.Raise(Settings);
		}
	}
}
