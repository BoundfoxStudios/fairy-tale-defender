using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement
{
	/// <summary>
	/// This component should be the very first component that is loaded by an initialization scene.
	/// It will handle bootstrapping the most necessary parts of the game to boot it.
	/// </summary>
	[AddComponentMenu(Constants.MenuNames.SceneManagement + "/" + nameof(InitializationLoader))]
	public class InitializationLoader : MonoBehaviour
	{
		[field: SerializeField]
		public SteamIntegrator SteamIntegrator { get; private set; } = default!;

		[field: Header("Scenes")]
		[field: SerializeField]
		private AssetReferenceT<PersistentManagersSceneSO> PersistentManagersScene { get; set; } = default!;

		[field: SerializeField]
		private AssetReferenceT<MenuSO> MainMenuScene { get; set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		private AssetReferenceT<LoadSceneEventChannelSO> LoadSceneEventChannel { get; set; } = default!;

		[field: Header("GameSettings Reference")]
		[field: SerializeField]
		private AssetReferenceT<SettingsSO> Settings { get; set; } = default!;

		[UsedImplicitly]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private async UniTaskVoid Start()
		{
			// We must start the Steam integration as soon as possible to have API available.
			// It could also be that the application will need to close and restart in order to run via Steam.
			SteamIntegrator.Integrate();

			await LoadSettingsAsync();

			var persistentManagersSceneInstance = await LoadPersistentManagersAsync();

				// Because we load the Steam API here and not via the PersistentManager scene, we move the GameObject there
				// in order to survive the unloading of the initialization scene.
			SceneManager.MoveGameObjectToScene(SteamIntegrator.gameObject, persistentManagersSceneInstance.Scene);
			SteamIntegrator.transform.SetAsFirstSibling();

			await LoadIntoMainMenuAsync();
			await UnloadInitializationSceneAsync();
		}

		private async UniTask LoadSettingsAsync()
		{
			var settings = await Settings.LoadAssetAsync();
			await settings.LoadAsync();
		}

		private async UniTask<SceneInstance> LoadPersistentManagersAsync()
		{
			var persistentManagersScene = await PersistentManagersScene.LoadAssetAsync();
			var sceneInstance = await persistentManagersScene.SceneReference.LoadSceneAsync(LoadSceneMode.Additive);

			return sceneInstance;
		}

		private async UniTask LoadIntoMainMenuAsync()
		{
			var mainMenuScene = await MainMenuScene.LoadAssetAsync();
			var loadSceneEventChannel = await LoadSceneEventChannel.LoadAssetAsync();

			loadSceneEventChannel.Raise(new()
			{
				Scene = mainMenuScene,
				ShowLoadingScreen = true
			});
		}

		private async UniTask UnloadInitializationSceneAsync()
		{
			// Initialization Scene will always have build index 0.
			await SceneManager.UnloadSceneAsync(0);
		}
	}
}
