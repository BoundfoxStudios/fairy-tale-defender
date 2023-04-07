using BoundfoxStudios.FairyTaleDefender.Systems.SpawnSystem.Waves;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Editors.SpawnSystem.Waves
{
	[CustomPropertyDrawer(typeof(Wave))]
	public class WaveEditor : PropertyDrawer
	{
		private static readonly GUIContent ErrorIcon = EditorGUIUtility.IconContent("Error");
		private readonly float _controlHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var copiedProperty = property.Copy();

			if (!copiedProperty.isExpanded)
			{
				return _controlHeight;
			}

			var additionalControls = copiedProperty.CountInProperty() - 1; // -1 for the property itself.

			return _controlHeight + additionalControls * _controlHeight;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent _)
		{
			var wave = (Wave)property.managedReferenceValue;

			var label =
				$"{wave.InspectorName} (Delay: {wave.DelayBetweenEachSpawnInSeconds:0.00} s; Total: {wave.TimeToSpawnAllEnemies:0.00} s)";

			EditorGUI.PropertyField(position, property, new(label, wave.IsValid ? null : ErrorIcon.image), true);
		}
	}
}
