using BoundfoxStudios.CommunityProject.Utilities;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Editor.GuiControls
{
	public static class MinMaxSlider
	{
		public static (float Min, float Max) DrawEditorGUILayout(string label, float min, float max, Vector2 limits, int roundToDigits = 2)
		{
			var numberFieldStyle = EditorStyles.numberField;
			var halfHorizontalPadding = numberFieldStyle.padding.horizontal / 2;

			// Start horizontal layout, so we can draw multiple controls on one row.
			EditorGUILayout.BeginHorizontal();

			// Get a rect for the whole row.
			var controlRect = EditorGUILayout.GetControlRect();
			controlRect = EditorGUI.PrefixLabel(controlRect, new(label));

			// Calculate a new rect for numberFields.
			// But: This rect will start at the end of the control rect, so we will do some calculations to adjust it.
			var fieldControlRect = GUILayoutUtility.GetRect(GUIContent.none, numberFieldStyle, GUILayout.MinWidth(20), GUILayout.MaxWidth(50));

			// Move the fieldControlRect to the beginning of the controlRect.
			// We have to add some padding to align the control with all other controls in the inspector.
			fieldControlRect.x -= controlRect.width + halfHorizontalPadding;

			// Move the controlRect to the end of the fieldControlRect and apply some padding.
			controlRect.x += fieldControlRect.width + halfHorizontalPadding;

			// Shrink the size of the controlRect, because we're going to draw another control afterwards.
			controlRect.width -= fieldControlRect.width + halfHorizontalPadding;

			min = EditorGUI.FloatField(fieldControlRect, min);

			EditorGUI.MinMaxSlider(controlRect, ref min, ref max, limits.x, limits.y);

			// Now, move the fieldControlRect at the end of the controlRect.
			fieldControlRect.x = controlRect.x + controlRect.width + halfHorizontalPadding;
			max = EditorGUI.FloatField(fieldControlRect, max);

			EditorGUILayout.EndHorizontal();

			if (min > max)
			{
				(min, max) = (max, min);
			}

			min = MathfUtilities.Round(Mathf.Max(min, limits.x), roundToDigits);
			max = MathfUtilities.Round(Mathf.Min(max, limits.y), roundToDigits);

			return (min, max);
		}
	}
}
