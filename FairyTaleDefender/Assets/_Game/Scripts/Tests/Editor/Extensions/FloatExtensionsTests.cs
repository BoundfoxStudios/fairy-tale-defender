using System;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace BoundfoxStudios.FairyTaleDefender.Tests.Editor.Extensions
{
	public class FloatExtensionTests
	{
		[TestCase(1.625f, -1)]
		public void Round_ShouldThrow_GivenNegativeDecimalsAmount(float value, int decimals)
		{
			Action action = () => value.Round(decimals);

			action.Should().Throw<ArgumentOutOfRangeException>();
		}

		[TestCase(1.625f, 2, 1.63f)]
		public void Round_ShouldRoundUp_GivenDecimalIsHigherOrEqualFive(float value, int decimals, float expectedResult)
		{
			var result = value.Round(decimals);

			result.Should().BeApproximately(expectedResult, decimals);
		}

		[TestCase(2.4453125f, 1, 2.4f)]
		public void Round_ShouldRoundDown_GivenDecimalIsLowerOrEqualFour(float value, int decimals, float expectedResult)
		{
			var result = value.Round(decimals);

			result.Should().BeApproximately(expectedResult, decimals);
		}

		[TestCase(1.5f, 0, 2f)]
		[TestCase(2.4453125f, 0, 2f)]
		public void Round_ShouldRoundToNextInteger_GivenZeroDecimals(float value, int decimals, float expectedResult)
		{
			var result = value.Round(decimals);

			result.Should().Be(expectedResult);
		}
	}
}
