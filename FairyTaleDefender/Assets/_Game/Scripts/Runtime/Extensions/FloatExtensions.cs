using System;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Extensions
{
	public static class FloatExtensions
	{
		/// <summary>
		/// Rounds the given <paramref name="value"/> with a specific amount of <paramref name="decimals"/> places.
		/// </summary>
		public static float Round(this float value, int decimals)
		{
			if (decimals < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(decimals), "Can not be smaller than 0");
			}

			var roundingMultiplier = Mathf.Pow(10, decimals);

			return Mathf.Round(value * roundingMultiplier) / roundingMultiplier;
		}
	}
}
