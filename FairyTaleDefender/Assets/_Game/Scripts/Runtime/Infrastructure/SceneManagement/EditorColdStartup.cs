using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement
{
	/// <summary>
	///   A cold start in the editor is when the user is in any scene except the initialization scene and enters the play mode.
	///   In this case the game does not pass the necessary initialization scene for bootstrapping everything.
	///   In this case this script will jump in and perform necessary bootstrapping actions.
	/// </summary>
	[AddComponentMenu(Constants.MenuNames.SceneManagement + "/" + nameof(EditorColdStartup))]
	public class EditorColdStartup : MonoBehaviour
	{
#if UNITY_EDITOR
		[field: Header("References")]
		[field: SerializeField]
		private SceneSO ThisScene { get; set; } = default!;

		[field: SerializeField]
		public SteamIntegrator SteamIntegrator { get; private set; } = default!;

		[field: SerializeField]
		private SettingsSO Settings { get; set; } = default!;

		[field: SerializeField]
		private PersistentManagersSceneSO PersistentManagersScene { get; set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		private LoadSceneEventChannelSO NotifyColdStartupEventChannel { get; set; } = default!;

		private bool _isColdStartup;

		private void Awake()
		{
			if (!SceneManager.GetSceneByName(PersistentManagersScene.SceneReference.editorAsset.name).isLoaded)
			{
				_isColdStartup = true;
			}
		}

		[UsedImplicitly]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private async UniTaskVoid Start()
		{
			if (!_isColdStartup)
			{
				return;
			}

			SteamIntegrator.Integrate();

			await Settings.LoadAsync();

			var persistentManagersSceneInstance = await PersistentManagersScene.SceneReference.LoadSceneAsync(LoadSceneMode.Additive);

			// Because we load the Steam API here and not via the PersistentManager scene, we move the GameObject there
			// in order to survive the unloading of the initialization scene.
			SteamIntegrator.transform.SetParent(null);
			SceneManager.MoveGameObjectToScene(SteamIntegrator.gameObject, persistentManagersSceneInstance.Scene);
			SteamIntegrator.transform.SetAsFirstSibling();

			// To avoid Cross Scene Reference Warning in editor mode
			SteamIntegrator = null!;

			if (ThisScene)
			{
				NotifyColdStartupEventChannel.Raise(new()
				{
					Scene = ThisScene,
					ShowLoadingScreen = false
				});

				return;
			}

			Debug.LogWarning($"This scene does not have {nameof(ThisScene)} set. You maybe forgot it!");
		}
#endif
	}
}
