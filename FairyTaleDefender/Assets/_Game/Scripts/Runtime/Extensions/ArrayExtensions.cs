using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Extensions
{
	public static class ArrayExtensions
	{
		public static T PickRandom<T>(this T[]? items)
		{
			Debug.Assert(items is not null, "Trying to pick a random from a non-existing array!");
			Debug.Assert(items!.Length > 0, "Trying to pick a random from an empty array!");

			return items[Random.Range(0, items.Length)];
		}
	}
}
