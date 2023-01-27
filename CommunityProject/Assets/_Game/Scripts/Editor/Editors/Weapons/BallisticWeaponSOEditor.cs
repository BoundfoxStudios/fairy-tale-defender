using System;
using BoundfoxStudios.CommunityProject.Editor.Extensions;
using BoundfoxStudios.CommunityProject.Editor.GuiControls;
using BoundfoxStudios.CommunityProject.Weapons.BallisticWeapons.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Editor.Editors.Weapons
{
	[CustomEditor(typeof(BallisticWeaponSO))]
	public class BallisticWeaponSOEditor : WeaponSOEditor
	{
		private SerializedProperty _minimumRangeProperty;
		private SerializedProperty _rotationSpeedInDegreesPerSecondProperty;
		private SerializedProperty _launchAnimationSpeedProperty;
		private SerializedProperty _launchEasingProperty;
		private SerializedProperty _rewindAnimationSpeedProperty;
		private SerializedProperty _rewindEasingProperty;

		private float _rangeMin;
		private float _rangeMax;

		private static GUIStyle HelpBoxRichTextStyle => new(EditorStyles.helpBox)
		{
			richText = true
		};

		protected override string[] ToolbarEntries { get; } = { "Parameters", "Animation" };

		protected override void OnEnable()
		{
			base.OnEnable();

			_minimumRangeProperty = serializedObject.FindRealProperty(nameof(BallisticWeaponSO.MinimumRange));
			_rotationSpeedInDegreesPerSecondProperty =
				serializedObject.FindRealProperty(nameof(BallisticWeaponSO.RotationSpeedInDegreesPerSecond));
			_launchAnimationSpeedProperty = serializedObject.FindRealProperty(nameof(BallisticWeaponSO.LaunchAnimationSpeed));
			_launchEasingProperty = serializedObject.FindRealProperty(nameof(BallisticWeaponSO.LaunchEasing));
			_rewindAnimationSpeedProperty = serializedObject.FindRealProperty(nameof(BallisticWeaponSO.RewindAnimationSpeed));
			_rewindEasingProperty = serializedObject.FindRealProperty(nameof(BallisticWeaponSO.RewindEasing));

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
			AttackAngleDiagram.Draw(controlRect, AttackAngleProperty.GetValue<int>());

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

		private void RenderAnimation()
		{
			EditorGUILayout.PropertyField(_rotationSpeedInDegreesPerSecondProperty);
			EditorGUILayout.PropertyField(_launchAnimationSpeedProperty);
			EditorGUILayout.PropertyField(_launchEasingProperty);
			EditorGUILayout.PropertyField(_rewindAnimationSpeedProperty);
			EditorGUILayout.PropertyField(_rewindEasingProperty);

			var launchAnimationSpeed = _launchAnimationSpeedProperty.GetValue<float>();
			var rewindAnimationSpeed = _rewindAnimationSpeedProperty.GetValue<float>();
			var fireRatePerSeconds = FireRateInSecondsProperty.GetValue<float>();

			var animationTime = launchAnimationSpeed + rewindAnimationSpeed;
			var idleTime = fireRatePerSeconds - animationTime;

			if (idleTime < 0)
			{
				EditorGUILayout.HelpBox($"Careful! The total time of animation " +
				                        $"({nameof(BallisticWeaponSO.LaunchAnimationSpeed)} + {nameof(BallisticWeaponSO.RewindAnimationSpeed)}; " +
				                        $"{launchAnimationSpeed:F2} s + {rewindAnimationSpeed:F2} s = {animationTime:F2} s) " +
				                        $"must be lower or equal than {nameof(BallisticWeaponSO.FireRateInSeconds)} ({fireRatePerSeconds:F2} s)", MessageType.Error);
				return;
			}

			EditorGUILayout.TextArea($"<b>Idle Time:</b> {idleTime:F2} s\n" +
			                        $"<b>Animation Time:</b> {animationTime:F2} s", HelpBoxRichTextStyle);
		}

		private void HelpBoxCodeError(string message)
		{
			EditorGUILayout.HelpBox($"{message} (This is an error in the code not the settings in the inspector)", MessageType.Error);
		}
	}
}
