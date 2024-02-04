using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace BoundfoxStudios.FairyTaleDefender.Extensions
{
	public static class StringExtensions
	{
		// From: https://stackoverflow.com/a/19937132/959687
		// white space, em-dash, en-dash, underscore
		static readonly Regex WordDelimiters = new(@"[\s—–_]", RegexOptions.Compiled);

		// characters that are not valid
		static readonly Regex InvalidChars = new(@"[^a-z0-9\-]", RegexOptions.Compiled);

		// multiple hyphens
		static readonly Regex MultipleHyphens = new(@"-{2,}", RegexOptions.Compiled);

		private static string ToSlug(string value)
		{
			// trim whitespace from start and end
			value = value.Trim();

			// convert to lower case
			value = value.ToLowerInvariant();

			// remove diacritics (accents)
			value = RemoveDiacritics(value);

			// ensure all word delimiters are hyphens
			value = WordDelimiters.Replace(value, "-");

			// strip out invalid characters
			value = InvalidChars.Replace(value, "");

			// replace multiple hyphens (-) with a single hyphen
			value = MultipleHyphens.Replace(value, "-");

			// trim hyphens (-) from ends
			return value.Trim('-');
		}

		/// See: http://www.siao2.com/2007/05/14/2629747.aspx
		private static string RemoveDiacritics(string stIn)
		{
			var stFormD = stIn.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder();

			foreach (var t in stFormD)
			{
				var uc = CharUnicodeInfo.GetUnicodeCategory(t);
				if (uc != UnicodeCategory.NonSpacingMark)
				{
					sb.Append(t);
				}
			}

			return (sb.ToString().Normalize(NormalizationForm.FormC));
		}

		public static string Slugify(this string input) => ToSlug(input);
	}
}
