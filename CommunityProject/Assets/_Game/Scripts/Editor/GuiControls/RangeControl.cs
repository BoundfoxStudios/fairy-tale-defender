using BoundfoxStudios.CommunityProject.Editor.Extensions;
using BoundfoxStudios.CommunityProject.Infrastructure;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Editor.GuiControls
{
	public static class RangeControl
	{
		public static void DrawEditorGUILayout(SerializedProperty minimumRangeProperty,
			SerializedProperty maximumRangeProperty)
		{
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

			var rangeLimits = new Limits2(rangeMinAttribute.min, rangeMaxAttribute.max);

			EditorGUI.BeginChangeCheck();

			var rangeMin = minimumRangeProperty.GetValue<float>();
			var rangeMax = maximumRangeProperty.GetValue<float>();

			(rangeMin, rangeMax) = MinMaxSlider.DrawEditorGUILayout("Range", rangeMin, rangeMax, rangeLimits);

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
			$"{message} (This is an error in the code, not the settings in the inspector)", MessageType.Error);
	}
}
