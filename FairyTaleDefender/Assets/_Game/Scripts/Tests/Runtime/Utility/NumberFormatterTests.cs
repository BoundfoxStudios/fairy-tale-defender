using BoundfoxStudios.FairyTaleDefender.Utility;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine.Localization.Settings;

namespace BoundfoxStudios.FairyTaleDefender.Tests.Utility
{
	public class NumberFormatterTests
	{
		[Test]
		public void Format_DisplaysSeparatorInCurrentCulture()
		{
			var value = -1.5f;
			var culture = LocalizationSettings.SelectedLocale.Identifier.CultureInfo;
			var expectedResult = value.ToString("0.##", culture);

			var result = NumberFormatter.Format(value);

			result.Should().Be(expectedResult);
		}
	}
}
