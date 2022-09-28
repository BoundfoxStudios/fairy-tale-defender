using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Editor.Windows.LevelSelectionEditorWindow
{
    public class LevelSelectionEditorWindow : EditorWindow
    {
		private bool _openAdditive;
		private Vector2 _scrollPosition;

		[MenuItem(Constants.MenuNames.LevelSelection)]
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
		}

		private void RenderMenuScenes()
		{
			var show = BeginFoldoutGroup("Menus");
			if (show)
			{
				EditorGUILayout.BeginHorizontal();
				OpenSceneButton("MainMenu", "Menus/MainMenu");
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
				OpenSceneButton("Initialization", "Managers/Initialization");
				OpenSceneButton("PersistentManagers", "Managers/PersistentManagers");
				EditorGUILayout.EndHorizontal();
			}
			EndFoldoutGroup("Managers", show);
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
	}
}
