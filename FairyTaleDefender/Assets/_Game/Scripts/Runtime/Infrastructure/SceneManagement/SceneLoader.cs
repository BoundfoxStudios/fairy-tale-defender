using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement
{
	[AddComponentMenu(Constants.MenuNames.SceneManagement + "/" + nameof(SceneLoader))]
	public class SceneLoader : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private GameplaySceneSO GameplayScene { get; set; } = default!;

		[field: SerializeField]
		private LevelRuntimeAnchorSO LevelRuntimeAnchor { get; set; } = default!;

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
			LevelRuntimeAnchor.Item = null;

			if (loadSceneData.ShowLoadingScreen)
			{
				ToggleLoadingScreenEventChannel.Raise(true);
			}

			await UnloadPreviousSceneAsync(loadSceneData);
			await HandleGameplaySceneAsync(loadSceneData.Scene);

			var sceneInstance = await loadSceneData.Scene.SceneReference.LoadSceneAsync(LoadSceneMode.Additive, true, 0);
			_currentlyLoadedScene = loadSceneData.Scene;

			InitializeScene(sceneInstance.Scene);

			if (loadSceneData.ShowLoadingScreen)
			{
				ToggleLoadingScreenEventChannel.Raise(false);
			}

			StartScene();
		}

		private async UniTask HandleGameplaySceneAsync(SceneSO scene)
		{
			switch (scene)
			{
				case MenuSO:
					// Unload the gameplay scene when we're switching to a menu.
					// Also don't wait for it to be unloaded so we can switch faster to the menu.
					UnloadGameplaySceneAsync().Forget();
					break;

				case LevelSO level:
					LevelRuntimeAnchor.Item = level;
					await UnloadGameplaySceneAsync();
					await LoadGameplaySceneAsync();
					break;
			}
		}

		private async UniTask LoadGameplaySceneAsync()
		{
			if (!GameplayScene.SceneReference.IsValid())
			{
				await GameplayScene.SceneReference.LoadSceneAsync(LoadSceneMode.Additive);
			}
		}

		private async UniTask UnloadGameplaySceneAsync()
		{
			if (GameplayScene.SceneReference.IsValid())
			{
				await GameplayScene.SceneReference.UnLoadScene();
			}
		}

		private void InitializeScene(Scene scene)
		{
			SceneManager.SetActiveScene(scene);
			LightProbes.TetrahedralizeAsync();
		}

		private void StartScene()
		{
			Time.timeScale = 1;
			SceneReadyEventChannel.Raise();
		}

		private async UniTask UnloadPreviousSceneAsync(LoadSceneData loadSceneData)
		{
			if (_currentlyLoadedScene is null)
			{
				return;
			}

			if (_currentlyLoadedScene.SceneReference.IsValid())
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
				SceneManager.UnloadSceneAsync(_currentlyLoadedScene.SceneReference.editorAsset.name).ToUniTask().Forget();
			}
#endif
		}

#if UNITY_EDITOR
		private void EditorColdStartup(LoadSceneEventChannelSO.EventArgs args) => EditorColdStartupAsync(args).Forget();

		private async UniTaskVoid EditorColdStartupAsync(LoadSceneEventChannelSO.EventArgs args)
		{
			_currentlyLoadedScene = args.Scene;

			if (_currentlyLoadedScene is LevelSO level)
			{
				LevelRuntimeAnchor.Item = level;
				await GameplayScene.SceneReference.LoadSceneAsync(LoadSceneMode.Additive);
			}

			StartScene();
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
