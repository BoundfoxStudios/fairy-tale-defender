using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Gizmos
{
	public static class ArrowHelper
	{
		public static void Draw(Vector3 from, Vector3 to)
			=> Draw(from, to, Color.green);

		public static void Draw(Vector3 from, Vector3 to, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 45)
		{
			var halfAngle = arrowHeadAngle / 2;
			var direction = (to - from).normalized;

			var right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + halfAngle, 0) *
			            new Vector3(0, 0, 1);
			var left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - halfAngle, 0) *
			           new Vector3(0, 0, 1);

			Debug.DrawLine(from, to, color);
			Debug.DrawRay(to, left * arrowHeadLength, color);
			Debug.DrawRay(to, right * arrowHeadLength, color);
		}
	}
}
