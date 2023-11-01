using BoundfoxStudios.FairyTaleDefender.Editor.Extensions;
using BoundfoxStudios.FairyTaleDefender.UI.Utility;
using UnityEditor;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Editors.UI
{
	[CustomEditor(typeof(MultiGraphicsTarget))]
	public class MultiGraphicsTargetEditor : UnityEditor.Editor
	{
		private SerializedProperty _targetsProperty = default!;

		private void OnEnable()
		{
			_targetsProperty = serializedObject.FindRealProperty(nameof(MultiGraphicsTarget.Targets));
		}

		public override void OnInspectorGUI()
		{
			EditorGUILayout.PropertyField(_targetsProperty);
		}
	}
}
