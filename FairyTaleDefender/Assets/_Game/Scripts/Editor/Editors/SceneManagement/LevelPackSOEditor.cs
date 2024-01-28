using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Editors.SceneManagement
{
	[CustomEditor(typeof(LevelPackSO))]
	public class LevelPackSOEditor : UnityEditor.Editor
	{
		private class DummySO : ScriptableObject
		{
			public LevelPackSO PreviousLevelPack = default!;
			public LevelPackSO NextLevelPack = default!;
		}

		private SerializedObject _serializedObject = default!;
		private SerializedProperty _previousLevelPack = default!;
		private SerializedProperty _nextLevelPack = default!;

		private void OnEnable()
		{
			var dummy = CreateInstance<DummySO>();
			_serializedObject = new SerializedObject(dummy);

			_previousLevelPack = _serializedObject.FindProperty(nameof(DummySO.PreviousLevelPack));
			_nextLevelPack = _serializedObject.FindProperty(nameof(DummySO.NextLevelPack));

			var levelPack = (LevelPackSO)target;
			dummy.NextLevelPack = levelPack.NextLevelPack!;
			dummy.PreviousLevelPack = levelPack.PreviousLevelPack!;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			_serializedObject.Update();
			GUI.enabled = false;
			EditorGUILayout.PropertyField(_previousLevelPack);
			EditorGUILayout.PropertyField(_nextLevelPack);
		}
	}
}
