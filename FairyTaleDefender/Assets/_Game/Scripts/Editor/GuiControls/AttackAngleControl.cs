using System;
using BoundfoxStudios.FairyTaleDefender.Editor.Extensions;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.GuiControls
{
	public static class AttackAngleControl
	{
		public static void DrawEditorGUILayout(SerializedProperty property)
		{
			EditorGUILayout.BeginHorizontal();

			const int diagramHeight = 50;
			var controlRect = EditorGUILayout.GetControlRect(true, diagramHeight);
			controlRect = EditorGUI.PrefixLabel(controlRect, new(property.displayName));

			var diagramRect = controlRect;
			// Make it square, in the Inspector 99,99 % it's wider than long, so we don't care about the other case
			diagramRect.width = diagramRect.height;

			switch (property.propertyType)
			{
				case SerializedPropertyType.Float:
					DrawDiagram(controlRect, property.GetValue<float>());
					break;

				case SerializedPropertyType.Integer:
					DrawDiagram(controlRect, property.GetValue<int>());
					break;

				default:
					throw new NotImplementedException($"Property type {property.propertyType} is not implemented yet.");
			}

			var halfPadding = EditorStyles.numberField.padding.horizontal / 2;
			controlRect.x += diagramRect.width + halfPadding;
			controlRect.width -= diagramRect.width + halfPadding;
			controlRect.height = EditorGUIUtility.singleLineHeight;
			controlRect.y += diagramHeight / 2f - EditorGUIUtility.singleLineHeight / 2;
			EditorGUI.PropertyField(controlRect, property, GUIContent.none);

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
		}

		public static void DrawDiagram(Rect rect, float attackAngle)
		{
			var radius = Mathf.Min(rect.width, rect.height) / 2;
			var baseX = rect.x + radius;
			var baseY = rect.y + radius;
			var center = new Vector3(baseX, baseY, 0);

			Handles.color = EditorStyles.label.normal.textColor;
			Handles.DrawSolidDisc(center, Vector3.forward, radius);

			var from = Quaternion.Euler(0, 0, -attackAngle / 2) * Vector3.down;

			Handles.color = Color.red;
			Handles.DrawSolidArc(center, Vector3.forward, from, attackAngle, radius);
		}
	}
}
