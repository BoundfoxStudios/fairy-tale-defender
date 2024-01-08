using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Localization.Settings;

namespace BoundfoxStudios.FairyTaleDefender.Utility
{
	/// <summary>
	/// Utility class for converting numbers into strings.
	/// </summary>
	public static class NumberFormatter
	{
		/// <summary>
		/// Returns the given <paramref name="value"/> as a string with two decimal places at most.
		/// </summary>
		/// <param name="value">The number that should be formatted.</param>
		/// <param name="keepTrailingDecimalZeros">If true displays last decimals even if they are zero, else they will be omitted if zero.</param>
		/// <param name="useInvariantCulture">Useful for e.g. testing, as separators aren't system dependent.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format(float value, bool keepTrailingDecimalZeros = false, bool useInvariantCulture = false)
		{
			var culture = useInvariantCulture ? CultureInfo.InvariantCulture : LocalizationSettings.SelectedLocale.Identifier.CultureInfo;
			var format = keepTrailingDecimalZeros ? "0.00" : "0.##";

			return value.ToString(format, culture);
		}
	}
}
