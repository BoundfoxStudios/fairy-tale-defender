using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement
{
	/// <summary>
	/// Component for requesting scene loads from e.g. a button.
	/// </summary>
	[AddComponentMenu(Constants.MenuNames.SceneManagement + "/" + nameof(SceneLoadRequester))]
	public class SceneLoadRequester : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		public SceneSO SceneToLoad { get; set; } = default!;

		[field: SerializeField]
		[field: Tooltip("Use this to load next or same level again from a generic button that has not to be in the same scene.")]
		public LevelRuntimeAnchorSO LevelRuntimeAnchor { get; private set; } = default!;

		[field: Header("Settings")]
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

		public void ReplayLevel()
		{
			if (!LevelRuntimeAnchor)
			{
				Debug.LogError($"No LevelRuntimeAnchor referenced on this object.", this);
				return;
			}

			LoadSceneEventChannel.Raise(new()
			{
				Scene = LevelRuntimeAnchor.ItemSafe,
				ShowLoadingScreen = ShowLoadingScreen
			});
		}

		public void LoadNextLevel()
		{
			if (!LevelRuntimeAnchor)
			{
				Debug.LogError($"No LevelRuntimeAnchor referenced on this object.", this);
				return;
			}

			var nextLevel = LevelRuntimeAnchor.ItemSafe.NextLevel!;
			if (!nextLevel.Exists())
			{
				gameObject.SetActive(false);
				return;
			}

			SceneToLoad = nextLevel;
			LoadSceneEventChannel.Raise(new()
			{
				Scene = SceneToLoad,
				ShowLoadingScreen = ShowLoadingScreen
			});
		}

		public bool HasNextLevel() => LevelRuntimeAnchor.ItemSafe.NextLevel;
	}
}
