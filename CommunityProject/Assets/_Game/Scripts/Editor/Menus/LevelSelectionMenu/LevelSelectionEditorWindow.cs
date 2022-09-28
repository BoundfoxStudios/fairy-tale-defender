using BoundfoxStudios.CommunityProject.SceneManagement;
using BoundfoxStudios.CommunityProject.SceneManagement.ScriptableObjects;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoundfoxStudios.CommunityProject.Editor.LevelSelectionEditorWindow
{
    public class LevelSelectionEditorWindow : EditorWindow
    {
		#region Private Fields
		private bool _openAdditive;
		private Vector2 _scrollPosition;

		#endregion

		#region EditorWindow Implementation
		[MenuItem(Constants.MenuNames.LevelSelectionEditorWindowName)]
		private static void ShowWindow()
		{
			var window = GetWindow<LevelSelectionEditorWindow>();
			window.titleContent = new GUIContent("Level Selection");
			window.Show();
		}

		private void OnGUI()
		{
			_openAdditive = IsControlPressed();
			EditorGUILayout.LabelField(
			  _openAdditive
				? "Will open scenes additively."
				: "Press control to open scenes additively."
			);

			_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
			RenderScenes();
			EditorGUILayout.EndScrollView();
		}

		#region Unity-Callbacks
		private void ModifierKeysChanged()
		{
			if (hasFocus)
			{
				Repaint();
			}
		}

		#endregion

		#endregion

		#region Private (Helper) Methods

		private void RenderScenes()
		{
			RenderMenus();
			RenderGameplay();
		}

		private void RenderMenus()
		{
			var showMenus = BeginFoldoutGroup("Menus");
			if (showMenus)
			{
				EditorGUILayout.BeginHorizontal();
				OpenSceneButton("MainMenu", "Menus/MainMenu");
				EditorGUILayout.EndHorizontal();
			}
			EndFoldoutGroup("Menus", showMenus);

			var showManagers = BeginFoldoutGroup("Managers");
			if (showManagers)
			{
				EditorGUILayout.BeginHorizontal();
				OpenSceneButton("Initialization", "Managers/Initialization");
				OpenSceneButton("PersistentManagers", "Managers/PersistentManagers");
				EditorGUILayout.EndHorizontal();
			}
			EndFoldoutGroup("Managers", showManagers);
		}

		private void RenderGameplay()
		{
			var show = BeginFoldoutGroup("GameplayLevel");
			if (show)
			{
				//EditorGUILayout.BeginHorizontal();
				//OpenSceneButton("to be added", string.Empty);
				//EditorGUILayout.EndHorizontal();
			}
			EndFoldoutGroup("GameplayLevel", show);
		}

		private bool IsControlPressed()
		{
			var currentEvent = Event.current;
			return currentEvent.control;
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

		private void OpenSceneButton(string label, string sceneName)
		{
			var sceneFile = $"Assets/_Game/Scenes/{sceneName}.unity";
			EditorGUI.BeginDisabledGroup(IsSceneOpen(sceneFile));
			if (GUILayout.Button(label))
			{
				if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
				{
					return;
				}
				EditorSceneManager.OpenScene(sceneFile, _openAdditive ? OpenSceneMode.Additive : OpenSceneMode.Single);
			}
			EditorGUI.EndDisabledGroup();
		}

		private bool IsSceneOpen(string sceneFile)
		{
			for (var i = 0; i < EditorSceneManager.sceneCount; i++)
			{
				if (EditorSceneManager.GetSceneAt(i).path == sceneFile)
				{
					return true;
				}
			}
			return false;
		}

		#endregion

	}
}
