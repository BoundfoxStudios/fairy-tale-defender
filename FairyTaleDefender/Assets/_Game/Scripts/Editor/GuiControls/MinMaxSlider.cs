using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.GuiControls
{
	public static class MinMaxSlider
	{
		/// <summary>
		/// Draws a MinMaxSlider.
		/// </summary>
		public static Limits2 DrawEditorGUILayout(string label, Limits2 values, Limits2 limits, int decimalPlaces = 2)
		{
			var numberFieldStyle = EditorStyles.numberField;
			var halfHorizontalPadding = numberFieldStyle.padding.horizontal / 2;

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

			var (minimum, maximum) = values;
			minimum = EditorGUI.FloatField(fieldControlRect, minimum);

			EditorGUI.MinMaxSlider(controlRect, ref minimum, ref maximum, limits.Minimum, limits.Maximum);

			// Now, move the fieldControlRect at the end of the controlRect.
			fieldControlRect.x = controlRect.x + controlRect.width + halfHorizontalPadding;
			maximum = EditorGUI.FloatField(fieldControlRect, maximum);

			EditorGUILayout.EndHorizontal();

			if (minimum > maximum)
			{
				(minimum, maximum) = (maximum, minimum);
			}

			minimum = Mathf.Max(minimum, limits.Minimum).Round(decimalPlaces);
			maximum = Mathf.Min(maximum, limits.Maximum).Round(decimalPlaces);

			return new(minimum, maximum);
		}
	}
}
