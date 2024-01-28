using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Editors.SceneManagement
{
	[CustomEditor(typeof(LevelSO))]
	public class LevelSOEditor : UnityEditor.Editor
	{
		private class DummyLevelSO : ScriptableObject
		{
			public LevelSO PreviousLevel = default!;
			public LevelSO NextLevel = default!;
		}

		private SerializedObject _serializedObject = default!;
		private SerializedProperty _previousLevelProperty = default!;
		private SerializedProperty _nextLevelProperty = default!;

		private void OnEnable()
		{
			var dummy = CreateInstance<DummyLevelSO>();
			_serializedObject = new SerializedObject(dummy);

			_previousLevelProperty = _serializedObject.FindProperty(nameof(dummy.PreviousLevel));
			_nextLevelProperty = _serializedObject.FindProperty(nameof(dummy.NextLevel));

			var level = (LevelSO)target;
			dummy.PreviousLevel = level.PreviousLevel!;
			dummy.NextLevel = level.NextLevel!;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			_serializedObject.Update();

			GUI.enabled = false;
			EditorGUILayout.PropertyField(_previousLevelProperty);
			EditorGUILayout.PropertyField(_nextLevelProperty);
		}
	}
}
