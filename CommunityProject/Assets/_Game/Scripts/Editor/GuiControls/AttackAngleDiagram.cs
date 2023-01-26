using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Editor.GuiControls
{
	public static class AttackAngleDiagram
	{
		public static void Draw(Rect rect, float attackAngle)
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
