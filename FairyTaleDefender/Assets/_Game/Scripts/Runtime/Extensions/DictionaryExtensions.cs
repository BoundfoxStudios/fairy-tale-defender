using System;
using System.Collections.Generic;

namespace BoundfoxStudios.FairyTaleDefender.Extensions
{
	public static class DictionaryExtensions
	{
		public static void TryAddLazy<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
			Func<TValue> valueGetter)
		{
			if (dictionary.ContainsKey(key))
			{
				return;
			}

			dictionary.Add(key, valueGetter());
		}
	}
}
