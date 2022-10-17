using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.SceneManagement.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Settings.ScriptableObjects;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoundfoxStudios.CommunityProject.SceneManagement
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
		[Header("References")]
		[SerializeField]
		private SceneSO ThisScene;
		[SerializeField]
		private SettingsSO Settings;

		[SerializeField]
		private PersistentManagersSceneSO PersistentManagersScene;

		[Header("Broadcasting Channels")]
		[SerializeField]
		private LoadSceneEventChannelSO NotifyColdStartupEventChannel;

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

			await Settings.LoadAsync();

			await PersistentManagersScene.SceneReference.LoadSceneAsync(LoadSceneMode.Additive);

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
