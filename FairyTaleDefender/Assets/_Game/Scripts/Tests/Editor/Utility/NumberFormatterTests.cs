using BoundfoxStudios.FairyTaleDefender.Utility;
using FluentAssertions;
using NUnit.Framework;

namespace BoundfoxStudios.FairyTaleDefender.Tests.Editor.Utility
{
	public class NumberFormatterTests
	{
		[TestCase(-1.0625f, "-1.06")]
		public void Format_KeepsOnlyTwoDecimals_GivenALongerNumber(float value, string expectedString)
		{
			var result = NumberFormatter.Format(value, useInvariantCulture: true);

			result.Should().Be(expectedString);
		}

		[TestCase(0.00f, "0")]
		[TestCase(0.50f, "0.5")]
		public void Format_OmitsTrailingDecimalsIfZero(float value, string expectedString)
		{
			var result = NumberFormatter.Format(value, useInvariantCulture: true);

			result.Should().Be(expectedString);
		}

		[TestCase(0.00f, "0.00")]
		[TestCase(0.50f, "0.50")]
		public void Format_KeepTrailingDecimalZeros_GivenTheyShouldBeDisplayed(float value, string expectedString)
		{
			var result = NumberFormatter.Format(value, true, true);

			result.Should().Be(expectedString);
		}
	}
}
