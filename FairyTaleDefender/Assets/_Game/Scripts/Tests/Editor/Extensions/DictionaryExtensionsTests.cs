using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace BoundfoxStudios.FairyTaleDefender.Tests.Editor.Extensions
{
	public class DictionaryExtensionsTests
	{
		[Test]
		public void TryAddLazy_DoesNotAddItemToDictionary_IfKeyAlreadyExists()
		{
			var sut = new Dictionary<int, string> { { 1, "1" } };
			var newEntry = 1;

			sut.TryAddLazy(newEntry, () => newEntry.ToString());

			sut.Count.Should().Be(1);
		}

		[Test]
		public void TryAddLazy_DoesAddItemToDictionary_IfKeyIsNew()
		{
			var sut = new Dictionary<int, string> { { 1, "1" } };
			var newEntry = 2;
			var kvp = new KeyValuePair<int, string>(newEntry, newEntry.ToString());

			sut.TryAddLazy(newEntry, () => newEntry.ToString());

			sut.Should().Contain(kvp);
		}

		[Test]
		public void TryAddLazy_DoesThrow_IfDictionaryIsNull()
		{
			Dictionary<int, string>? sut = null;

			var newEntry = 2;
			TestDelegate action = () => sut!.TryAddLazy(newEntry, () => newEntry.ToString());

			Assert.Throws<NullReferenceException>(action);
		}
	}
}
