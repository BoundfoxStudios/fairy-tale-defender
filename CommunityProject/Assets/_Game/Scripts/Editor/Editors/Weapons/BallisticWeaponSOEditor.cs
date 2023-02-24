using System;
using BoundfoxStudios.CommunityProject.Editor.Extensions;
using BoundfoxStudios.CommunityProject.Editor.GuiControls;
using BoundfoxStudios.CommunityProject.Entities.Weapons.BallisticWeapons.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Editor.Editors.Weapons
{
	[CustomEditor(typeof(BallisticWeaponSO))]
	public class BallisticWeaponSOEditor : WeaponSOEditor
	{
		private SerializedProperty _minimumRangeProperty = default!;
		private SerializedProperty _rotationSpeedInDegreesPerSecondProperty = default!;
		private SerializedProperty _launchAnimationTimeInSecondsProperty = default!;
		private SerializedProperty _launchEasingProperty = default!;
		private SerializedProperty _rewindAnimationTimeInSecondsProperty = default!;
		private SerializedProperty _rewindEasingProperty = default!;

		private static GUIStyle? _helpBoxRichTextStyle;
		private static GUIStyle HelpBoxRichTextStyle => _helpBoxRichTextStyle ??= new(EditorStyles.helpBox)
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
			_launchAnimationTimeInSecondsProperty = serializedObject.FindRealProperty(nameof(BallisticWeaponSO.LaunchAnimationTimeInSeconds));
			_launchEasingProperty = serializedObject.FindRealProperty(nameof(BallisticWeaponSO.LaunchEasing));
			_rewindAnimationTimeInSecondsProperty = serializedObject.FindRealProperty(nameof(BallisticWeaponSO.RewindAnimationTimeInSeconds));
			_rewindEasingProperty = serializedObject.FindRealProperty(nameof(BallisticWeaponSO.RewindEasing));
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
			AttackAngleControl.DrawEditorGUILayout(AttackAngleProperty);
			RangeControl.DrawEditorGUILayout(_minimumRangeProperty, RangeProperty);
		}

		private void RenderAnimation()
		{
			EditorGUILayout.PropertyField(_rotationSpeedInDegreesPerSecondProperty);
			EditorGUILayout.PropertyField(_launchAnimationTimeInSecondsProperty);
			EditorGUILayout.PropertyField(_launchEasingProperty);
			EditorGUILayout.PropertyField(_rewindAnimationTimeInSecondsProperty);
			EditorGUILayout.PropertyField(_rewindEasingProperty);

			var launchAnimationSpeed = _launchAnimationTimeInSecondsProperty.GetValue<float>();
			var rewindAnimationSpeed = _rewindAnimationTimeInSecondsProperty.GetValue<float>();
			var fireRatePerSeconds = FireRateInSecondsProperty.GetValue<float>();

			var animationTime = launchAnimationSpeed + rewindAnimationSpeed;
			var idleTime = fireRatePerSeconds - animationTime;

			if (idleTime < 0)
			{
				EditorGUILayout.HelpBox($"Careful! The total time of animation " +
										$"({nameof(BallisticWeaponSO.LaunchAnimationTimeInSeconds)} + {nameof(BallisticWeaponSO.RewindAnimationTimeInSeconds)}; " +
										$"{launchAnimationSpeed:F2} s + {rewindAnimationSpeed:F2} s = {animationTime:F2} s) " +
										$"must be lower or equal than {nameof(BallisticWeaponSO.FireRateInSeconds)} ({fireRatePerSeconds:F2} s)", MessageType.Error);
				return;
			}

			EditorGUILayout.TextArea($"<b>Idle Time:</b> {idleTime:F2} s\n" +
									$"<b>Animation Time:</b> {animationTime:F2} s", HelpBoxRichTextStyle);
		}
	}
}
