using System;
using BoundfoxStudios.FairyTaleDefender.Editor.Extensions;
using BoundfoxStudios.FairyTaleDefender.Editor.GuiControls;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.DirectWeapons.ScriptableObjects;
using UnityEditor;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Editors.Weapons
{
	[CustomEditor(typeof(DirectWeaponSO))]
	public class DirectWeaponSOEditor : WeaponSOEditor
	{
		private SerializedProperty _rotationSpeedInDegreesPerSecondProperty = default!;
		private SerializedProperty _rewindAnimationTimeInSecondsProperty = default!;

		protected override string[] ToolbarEntries { get; } = { "Parameters", "Animation" };

		protected override void OnEnable()
		{
			base.OnEnable();

			_rotationSpeedInDegreesPerSecondProperty =
				serializedObject.FindRealProperty(nameof(DirectWeaponSO.RotationSpeedInDegreesPerSecond));
			_rewindAnimationTimeInSecondsProperty = serializedObject.FindRealProperty(nameof(BallisticWeaponSO.RewindAnimationTimeInSeconds));
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
			EditorGUILayout.PropertyField(FireRateEverySecondsProperty);
			AttackAngleControl.DrawEditorGUILayout(AttackAngleProperty);
			EditorGUILayout.PropertyField(RangeProperty);
		}

		private void RenderAnimation()
		{
			EditorGUILayout.PropertyField(_rotationSpeedInDegreesPerSecondProperty);
			EditorGUILayout.PropertyField(_rewindAnimationTimeInSecondsProperty);
		}
	}
}
