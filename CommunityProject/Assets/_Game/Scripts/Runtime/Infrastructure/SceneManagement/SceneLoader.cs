using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Infrastructure.SceneManagement.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoundfoxStudios.CommunityProject.Infrastructure.SceneManagement
{
	[AddComponentMenu(Constants.MenuNames.SceneManagement + "/" + nameof(SceneLoader))]
	public class SceneLoader : MonoBehaviour
	{
		[field: Header("Listening Channels")]
		[field: SerializeField]
		private LoadSceneEventChannelSO LoadSceneEventChannel { get; set; } = default!;

		[field: SerializeField]
		private LoadSceneEventChannelSO NotifyEditorColdStartupEventChannel { get; set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		private VoidEventChannelSO SceneReadyEventChannel { get; set; } = default!;

		[field: SerializeField]
		private BoolEventChannelSO ToggleLoadingScreenEventChannel { get; set; } = default!;

		private SceneSO? _currentlyLoadedScene;

		private void OnEnable()
		{
			LoadSceneEventChannel.Raised += LoadScene;

#if UNITY_EDITOR
			NotifyEditorColdStartupEventChannel.Raised += EditorColdStartup;
#endif
		}

		private void OnDisable()
		{
			LoadSceneEventChannel.Raised -= LoadScene;

#if UNITY_EDITOR
			NotifyEditorColdStartupEventChannel.Raised -= EditorColdStartup;
#endif
		}

		private void LoadScene(LoadSceneEventChannelSO.EventArgs args)
		{
			LoadSceneAsync(new()
			{
				Scene = args.Scene,
				ShowLoadingScreen = args.ShowLoadingScreen
			}).Forget();
		}

		private async UniTaskVoid LoadSceneAsync(LoadSceneData loadSceneData)
		{
			if (loadSceneData.ShowLoadingScreen)
			{
				ToggleLoadingScreenEventChannel.Raise(true);
			}

			await UnloadPreviousSceneAsync(loadSceneData);

			var sceneInstance = await loadSceneData.Scene.SceneReference.LoadSceneAsync(LoadSceneMode.Additive, true, 0);
			_currentlyLoadedScene = loadSceneData.Scene;

			InitializeScene(sceneInstance.Scene);

			if (loadSceneData.ShowLoadingScreen)
			{
				ToggleLoadingScreenEventChannel.Raise(false);
			}

			StartScene();
		}

		private void InitializeScene(Scene scene)
		{
			SceneManager.SetActiveScene(scene);
			LightProbes.TetrahedralizeAsync();
		}

		private void StartScene()
		{
			SceneReadyEventChannel.Raise();
		}

		private async UniTask UnloadPreviousSceneAsync(LoadSceneData loadSceneData)
		{
			if (_currentlyLoadedScene is null)
			{
				return;
			}

			if (_currentlyLoadedScene.SceneReference.OperationHandle.IsValid())
			{
				var unloadSceneOperation = _currentlyLoadedScene.SceneReference.UnLoadScene();

				if (loadSceneData.WaitForUnloadToBeDone)
				{
					await unloadSceneOperation;
				}
			}
#if UNITY_EDITOR
			else
			{
				// This will happen on editor cold startup and the player switches to another scene.
#pragma warning disable CS4014
				// Disable the warning that we're not awaiting an async operation here, we simply don't care. :)
				SceneManager.UnloadSceneAsync(_currentlyLoadedScene.SceneReference.editorAsset.name);
#pragma warning restore CS4014
			}
#endif
		}

#if UNITY_EDITOR
		private void EditorColdStartup(LoadSceneEventChannelSO.EventArgs args)
		{
			_currentlyLoadedScene = args.Scene;
		}
#endif

		private struct LoadSceneData
		{
			public SceneSO Scene;
			public bool ShowLoadingScreen;
			public bool WaitForUnloadToBeDone;
		}
	}
}
