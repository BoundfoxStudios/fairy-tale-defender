using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Extensions
{
	public static class VectorExtensions
	{
		public static Vector2 ToXZ(this Vector3 vector) => new(vector.x, vector.z);
		public static float DistanceSquaredTo(this Vector3 a, Vector3 b) => Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2);
		public static float DistanceSquaredTo(this Vector2 a, Vector2 b) => Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2);
	}
}
