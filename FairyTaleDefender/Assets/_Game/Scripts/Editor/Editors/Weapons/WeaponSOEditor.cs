using BoundfoxStudios.FairyTaleDefender.Editor.Extensions;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Editors.Weapons
{
	public abstract class WeaponSOEditor : UnityEditor.Editor
	{
		protected SerializedProperty RangeProperty = default!;
		protected SerializedProperty FireRateEverySecondsProperty = default!;
		protected SerializedProperty AttackAngleProperty = default!;

		protected abstract string[] ToolbarEntries { get; }

		private int _selectedToolbar;

		protected virtual void OnEnable()
		{
			RangeProperty = serializedObject.FindRealProperty(nameof(WeaponSO.Range));
			FireRateEverySecondsProperty = serializedObject.FindRealProperty(nameof(WeaponSO.FireRateEverySeconds));
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
