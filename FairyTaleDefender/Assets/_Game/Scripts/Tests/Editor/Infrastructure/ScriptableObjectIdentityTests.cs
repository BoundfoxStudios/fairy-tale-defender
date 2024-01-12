using BoundfoxStudios.FairyTaleDefender.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace BoundfoxStudios.FairyTaleDefender.Tests.Editor.Infrastructure
{
	public class ScriptableObjectIdentityTests
	{
		[Test]
		public void Equals_ShouldBeTrue_IfTheyAreTheSameInstance()
		{
			var identity = new ScriptableObjectIdentity { Guid = "unit-test-guid" };

			identity.Should().Be(identity);
		}

		[Test]
		public void Equals_ShouldBeTrue_IfTheyAreDifferentInstancesWithSameGuid()
		{
			var identity1 = new ScriptableObjectIdentity { Guid = "unit-test-guid" };
			var identity2 = new ScriptableObjectIdentity { Guid = "unit-test-guid" };

			identity1.Should().Be(identity2);
		}

		[Test]
		public void Equals_ShouldNotBeTrue_IfTheyAreDifferentInstancesWithDifferentGuid()
		{
			var identity1 = new ScriptableObjectIdentity { Guid = "unit-test-guid" };
			var identity2 = new ScriptableObjectIdentity { Guid = "different-unit-test-guid" };

			identity1.Should().NotBe(identity2);
		}

		[Test]
		public void CompareWithDoubleEqualOperator_ShouldBeEqual_IfTheyAreTheSameInstance()
		{
			var identity = new ScriptableObjectIdentity { Guid = "unit-test-guid" };

#pragma warning disable CS1718
			// Justification: we want to test this here
			// ReSharper disable once EqualExpressionComparison
			(identity == identity).Should().BeTrue();
#pragma warning restore CS1718
		}

		[Test]
		public void CompareWithDoubleEqualOperator_ShouldBeEqual_IfTheyAreDifferentInstancesWithSameGuid()
		{
			var identity1 = new ScriptableObjectIdentity { Guid = "unit-test-guid" };
			var identity2 = new ScriptableObjectIdentity { Guid = "unit-test-guid" };

			(identity1 == identity2).Should().BeTrue();
		}

		[Test]
		public void CompareWithNotEqualOperator_ShouldNotBeEqual_IfTheyAreDifferentInstancesWithDifferentGuid()
		{
			var identity1 = new ScriptableObjectIdentity { Guid = "unit-test-guid" };
			var identity2 = new ScriptableObjectIdentity { Guid = "different-unit-test-guid" };

			(identity1 != identity2).Should().BeTrue();
		}
	}
}
