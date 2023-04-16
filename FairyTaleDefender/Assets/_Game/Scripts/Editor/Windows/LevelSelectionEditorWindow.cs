using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Windows
{
	public class LevelSelectionEditorWindow : EditorWindow
	{
		private bool _openAdditive;
		private bool _selectAsset;
		private Vector2 _scrollPosition;

		private readonly AssetLocator<AllLevelPacksSO> _allLevelPacksLocator =
			new("ScriptableObjects/Scenes/Levels/AllLevelPacks.asset");

		private AllLevelPacksSO? _allLevelPacks;

		[MenuItem(Constants.MenuNames.Windows + "/Level Selection", priority = 10000)]
		private static void ShowWindow()
		{
			var window = GetWindow<LevelSelectionEditorWindow>();
			window.titleContent = new("Level Selection");
			window.Show();
		}

		private void OnEnable()
		{
			_allLevelPacksLocator.SafeInvokeAsync(allLevelPacks =>
			{
				_allLevelPacks = allLevelPacks;
				Repaint();
			}).Forget();
		}

		private void OnGUI()
		{
			_openAdditive = IsControlPressed();
			_selectAsset = IsAltPressed();

			EditorGUILayout.LabelField(
				_openAdditive
					? "Will open scenes additively."
					:
					_selectAsset
						? "Will select the Scene in project view"
						: "Press control to open scenes additively. Press alt to select the scene in project view."
			);

			_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
			RenderScenes();
			EditorGUILayout.EndScrollView();
		}

		private void ModifierKeysChanged()
		{
			if (hasFocus)
			{
				Repaint();
			}
		}

		private void RenderScenes()
		{
			RenderMenuScenes();
			RenderManagerScenes();
			RenderLevelPacks();
			RenderDevelopmentScenes();
		}

		private void RenderLevelPacks()
		{
			if (_allLevelPacks is null)
			{
				EditorGUILayout.HelpBox("AllLevelPacks could not be located. Check console for potential errors",
					MessageType.Warning);
				return;
			}

			foreach (var levelPack in _allLevelPacks.LevelPacks)
			{
				RenderLevelPack(levelPack);
			}
		}

		private void RenderLevelPack(LevelPackSO levelPack)
		{
			var show = BeginFoldoutGroup(levelPack.name);

			if (!show)
			{
				EndFoldoutGroup(levelPack.name, show);
				return;
			}

			var needsToEndHorizontal = false;

			var allLevels = levelPack.Levels;
			for (var index = 0; index < allLevels.Length; index++)
			{
				if (index % 3 == 0)
				{
					needsToEndHorizontal = true;
					EditorGUILayout.BeginHorizontal();
				}

				var level = allLevels[index];

				EditorGUI.BeginDisabledGroup(SceneManager.GetActiveScene().name == level.SceneReference.editorAsset.name);

				if (OpenSceneButton(level.name, AssetDatabase.GetAssetPath(level.SceneReference.editorAsset)))
				{
					Selection.activeObject = level;
				}

				EditorGUI.EndDisabledGroup();

				if (index % 3 == 2)
				{
					needsToEndHorizontal = false;
					EditorGUILayout.EndHorizontal();
				}
			}

			if (needsToEndHorizontal)
			{
				EditorGUILayout.EndHorizontal();
			}

			EndFoldoutGroup(levelPack.name, show);
		}

		private void RenderDevelopmentScenes()
		{
			var show = BeginFoldoutGroup("Development");

			if (show)
			{
				EditorGUILayout.HelpBox("The scenes in this group are not included in the build and are for development only.",
					MessageType.Info);
				EditorGUILayout.BeginHorizontal();
				OpenSceneByNameButton("Models", "Development/Models");
				EditorGUILayout.EndHorizontal();
			}

			EndFoldoutGroup("Development", show);
		}

		private void RenderMenuScenes()
		{
			var show = BeginFoldoutGroup("Menus");
			if (show)
			{
				EditorGUILayout.BeginHorizontal();
				OpenSceneByNameButton("MainMenu", "Menus/MainMenu");
				OpenSceneByNameButton("Credits", "Menus/Credits");
				EditorGUILayout.EndHorizontal();
			}

			EndFoldoutGroup("Menus", show);
		}

		private void RenderManagerScenes()
		{
			var show = BeginFoldoutGroup("Managers");
			if (show)
			{
				EditorGUILayout.BeginHorizontal();
				OpenSceneByNameButton("Initialization", "Managers/Initialization");
				OpenSceneByNameButton("PersistentManagers", "Managers/PersistentManagers");
				OpenSceneByNameButton("Gameplay", "Managers/Gameplay");
				EditorGUILayout.EndHorizontal();
			}

			EndFoldoutGroup("Managers", show);
		}

		private bool IsControlPressed()
		{
			var currentEvent = Event.current;
			return currentEvent.control;
		}

		private bool IsAltPressed()
		{
			var currentEvent = Event.current;
			return currentEvent.alt;
		}

		private bool BeginFoldoutGroup(string groupName)
		{
			var show = EditorPrefs.GetBool($"foldout_{groupName}", false);
			return EditorGUILayout.BeginFoldoutHeaderGroup(show, groupName);
		}

		private void EndFoldoutGroup(string groupName, bool show)
		{
			EditorGUILayout.EndFoldoutHeaderGroup();
			EditorPrefs.SetBool($"foldout_{groupName}", show);
		}

		private void OpenSceneByNameButton(string label, string sceneName) =>
			OpenSceneButton(label, $"Assets/_Game/Scenes/{sceneName}.unity");

		private bool OpenSceneButton(string label, string sceneFile)
		{
			EditorGUI.BeginDisabledGroup(IsSceneOpen(sceneFile));
			if (GUILayout.Button(label))
			{
				if (_selectAsset)
				{
					SearchUtils.PingAsset(sceneFile);
					return false;
				}

				if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
				{
					return false;
				}

				EditorSceneManager.OpenScene(sceneFile, _openAdditive ? OpenSceneMode.Additive : OpenSceneMode.Single);
				return true;
			}

			EditorGUI.EndDisabledGroup();
			return false;
		}

		private bool IsSceneOpen(string sceneFile)
		{
			for (var i = 0; i < SceneManager.sceneCount; i++)
			{
				if (SceneManager.GetSceneAt(i).path == sceneFile)
				{
					return true;
				}
			}

			return false;
		}
	}
}
