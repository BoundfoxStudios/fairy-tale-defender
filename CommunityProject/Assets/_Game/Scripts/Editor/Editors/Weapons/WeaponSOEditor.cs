using BoundfoxStudios.CommunityProject.Editor.Extensions;
using BoundfoxStudios.CommunityProject.Weapons.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Editor.Editors.Weapons
{
	public abstract class WeaponSOEditor : UnityEditor.Editor
	{
		protected SerializedProperty RangeProperty = default!;
		protected SerializedProperty FireRateInSecondsProperty = default!;
		protected SerializedProperty AttackAngleProperty = default!;

		protected abstract string[] ToolbarEntries { get; }

		private int _selectedToolbar;

		protected virtual void OnEnable()
		{
			RangeProperty = serializedObject.FindRealProperty(nameof(WeaponSO.Range));
			FireRateInSecondsProperty = serializedObject.FindRealProperty(nameof(WeaponSO.FireRateInSeconds));
			AttackAngleProperty = serializedObject.FindRealProperty(nameof(WeaponSO.AttackAngle));
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			_selectedToolbar = GUILayout.Toolbar(_selectedToolbar, ToolbarEntries);
			RenderToolbar(_selectedToolbar);

			serializedObject.ApplyModifiedProperties();
		}

		protected abstract void RenderToolbar(int selectedToolbar);
	}
}
