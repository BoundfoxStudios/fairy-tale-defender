using System;
using BoundfoxStudios.CommunityProject.Editor.Extensions;
using BoundfoxStudios.CommunityProject.Editor.GuiControls;
using BoundfoxStudios.CommunityProject.Weapons.BallisticWeapons.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Weapons.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Editor.Editors.Weapons
{
	[CustomEditor(typeof(BallisticWeaponSO))]
	public class BallisticWeaponSOEditor : WeaponSOEditor
	{
		private SerializedProperty _minimumRangeProperty;

		private float _rangeMin;
		private float _rangeMax;

		protected override string[] ToolbarEntries { get; } = { "Parameters", "Animation" };

		protected override void OnEnable()
		{
			base.OnEnable();

			_minimumRangeProperty = serializedObject.FindRealProperty(nameof(BallisticWeaponSO.MinimumRange));

			_rangeMin = _minimumRangeProperty.GetValue<float>();
			_rangeMax = RangeProperty.GetValue<float>();
		}

		protected override void RenderToolbar(int selectedToolbar)
		{
			switch (selectedToolbar)
			{
				case 0:
					RenderParameters();
					break;

				case 1:
					RenderAnimation();
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(selectedToolbar));
			}
		}

		private void RenderParameters()
		{
			EditorGUILayout.PropertyField(FireRateInSecondsProperty);

			RenderAttackAngle();
			RenderRange();
		}

		private void RenderAttackAngle()
		{
			EditorGUILayout.BeginHorizontal();

			const int diagramHeight = 50;
			var controlRect = EditorGUILayout.GetControlRect(true, diagramHeight);
			controlRect = EditorGUI.PrefixLabel(controlRect, new(AttackAngleProperty.displayName));

			var diagramRect = controlRect;
			// Make it square, in the Inspector 99,99 % it's wider than long, so we don't care about the other case
			diagramRect.width = diagramRect.height;
			AttackAngleDiagram.Draw(controlRect, AttackAngleProperty.GetValue<float>());

			var halfPadding = EditorStyles.numberField.padding.horizontal / 2;
			controlRect.x += diagramRect.width + halfPadding;
			controlRect.width -= diagramRect.width + halfPadding;
			controlRect.height = EditorGUIUtility.singleLineHeight;
			controlRect.y += diagramHeight / 2f - EditorGUIUtility.singleLineHeight / 2;
			EditorGUI.PropertyField(controlRect, AttackAngleProperty, GUIContent.none);

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
		}

		private void RenderRange()
		{
			var rangeLimits = Vector2.zero;

			if (!_minimumRangeProperty.TryGetAttribute<RangeAttribute>(out var rangeMinAttribute))
			{
				EditorGUILayout.HelpBox($"{nameof(BallisticWeaponSO.MinimumRange)} does not have a RangeAttribute!", MessageType.Error);
				return;
			}

			if (!RangeProperty.TryGetAttribute<RangeAttribute>(out var rangeMaxAttribute))
			{
				EditorGUILayout.HelpBox($"{nameof(BallisticWeaponSO.Range)} does not have a RangeAttribute!", MessageType.Error);
				return;
			}

			if (rangeMinAttribute.min >= rangeMaxAttribute.max)
			{
				HelpBoxCodeError($"{nameof(BallisticWeaponSO.MinimumRange)} minimum must not be greater than or equal to {nameof(BallisticWeaponSO.Range)} maximum!");
				return;
			}

			if (rangeMinAttribute.max <= rangeMaxAttribute.min)
			{
				HelpBoxCodeError($"{nameof(BallisticWeaponSO.MinimumRange)} maximum must be smaller than {nameof(BallisticWeaponSO.Range)}!");
				return;
			}

			rangeLimits.x = rangeMinAttribute.min;
			rangeLimits.y = rangeMaxAttribute.max;

			EditorGUI.BeginChangeCheck();

			(_rangeMin, _rangeMax) = MinMaxSlider.ForFloat("Range", _rangeMin, _rangeMax, rangeLimits);

			if (EditorGUI.EndChangeCheck())
			{
				_minimumRangeProperty.SetValue(_rangeMin);
				RangeProperty.SetValue(_rangeMax);
			}
		}

		private void RenderAnimation() { }

		private void HelpBoxCodeError(string message)
		{
			EditorGUILayout.HelpBox($"{message} (This is an error in the code not the settings in the inspector)", MessageType.Error);
		}
	}
}
