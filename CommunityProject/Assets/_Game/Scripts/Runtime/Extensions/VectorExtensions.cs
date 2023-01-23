using System.Runtime.CompilerServices;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Extensions
{
	public static class VectorExtensions
	{
		public static Vector2 ToXZ(this Vector3 vector) => new(vector.x, vector.z);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquaredTo(this Vector3 a, Vector3 b) => (b - a).sqrMagnitude;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquaredTo(this Vector2 a, Vector2 b) => (b - a).sqrMagnitude;
	}
}
