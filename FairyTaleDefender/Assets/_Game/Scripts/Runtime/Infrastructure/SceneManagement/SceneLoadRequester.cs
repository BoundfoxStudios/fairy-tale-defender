using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement
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
