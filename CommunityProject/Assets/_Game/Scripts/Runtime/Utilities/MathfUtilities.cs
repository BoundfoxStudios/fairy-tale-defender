using System;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Utilities
{
	public static class MathfUtilities
	{
		public static float Round(float value, int decimals)
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
