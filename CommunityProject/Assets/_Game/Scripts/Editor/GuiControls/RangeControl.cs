using BoundfoxStudios.CommunityProject.Editor.Extensions;
using BoundfoxStudios.CommunityProject.Weapons.BallisticWeapons.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Editor.GuiControls
{
	public static class RangeControl
	{
		public static void DrawEditorGUILayout(SerializedProperty minimumRangeProperty,
			SerializedProperty maximumRangeProperty)
		{
			var rangeLimits = Vector2.zero;

			if (!minimumRangeProperty.TryGetAttribute<RangeAttribute>(out var rangeMinAttribute))
			{
				NoRangeAttributeHelpBox(minimumRangeProperty);
				return;
			}

			if (!maximumRangeProperty.TryGetAttribute<RangeAttribute>(out var rangeMaxAttribute))
			{
				NoRangeAttributeHelpBox(maximumRangeProperty);
				return;
			}

			if (rangeMinAttribute.min >= rangeMaxAttribute.max)
			{
				CodeErrorHelpBox(
					$"{minimumRangeProperty.GetReadablePropertyPath()} [Range] minimum must not be greater than or equal to {maximumRangeProperty.GetReadablePropertyPath()} [Range] maximum!");
				return;
			}

			if (rangeMinAttribute.max <= rangeMaxAttribute.min)
			{
				CodeErrorHelpBox(
					$"{minimumRangeProperty.GetReadablePropertyPath()} [Range] maximum must be smaller than {maximumRangeProperty.GetReadablePropertyPath()} [Range] minimum!");
				return;
			}

			rangeLimits.x = rangeMinAttribute.min;
			rangeLimits.y = rangeMaxAttribute.max;

			EditorGUI.BeginChangeCheck();

			var rangeMin = minimumRangeProperty.GetValue<float>();
			var rangeMax = maximumRangeProperty.GetValue<float>();

			(rangeMin, rangeMax) = MinMaxSlider.ForFloat("Range", rangeMin, rangeMax, rangeLimits);

			if (EditorGUI.EndChangeCheck())
			{
				minimumRangeProperty.SetValue(rangeMin);
				maximumRangeProperty.SetValue(rangeMax);
			}
		}

		private static void NoRangeAttributeHelpBox(SerializedProperty property) => EditorGUILayout.HelpBox(
			$"{property.GetReadablePropertyPath()} does not have a RangeAttribute!",
			MessageType.Error);

		private static void CodeErrorHelpBox(string message) => EditorGUILayout.HelpBox(
			$"{message} (This is an error in the code not the settings in the inspector)", MessageType.Error);
	}
}
