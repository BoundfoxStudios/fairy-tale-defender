using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Extensions
{
	public static class ArrayExtensions
	{
		/// <summary>
		/// Picks a random item in this array.
		/// </summary>
		public static T? PickRandom<T>(this T[]? items)
		{
			return PickRandom(items, items!.Length);
		}

		/// <summary>
		/// Picks a random item in this array within the first element and the last element before given index <paramref name="maxExclusive"/>.
		/// </summary>
		public static T? PickRandom<T>(this T[]? items, int maxExclusive)
		{
			Debug.Assert(items is not null, "Trying to pick a random from a non-existing array!");
			Debug.Assert(items!.Length > 0, "Trying to pick a random from an empty array!");

			return items[Random.Range(0, maxExclusive)];
		}
	}
}
