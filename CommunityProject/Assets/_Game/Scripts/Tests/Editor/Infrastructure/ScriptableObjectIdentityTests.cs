using BoundfoxStudios.CommunityProject.Infrastructure;
using NUnit.Framework;
using FluentAssertions;

namespace BoundfoxStudios.CommunityProject.Tests.Editor.Infrastructure
{
	public class ScriptableObjectIdentityTests
	{
		[Test]
		public void ShouldBeEqualIfTheyAreTheSameInstance()
		{
			var identity = new ScriptableObjectIdentity { Guid = "unit-test-guid" };

			identity.Should().Be(identity);
		}

		[Test]
		public void ShouldBeEqualIfTheyAreDifferentInstancesHavingTheSameGuid()
		{
			var identity1 = new ScriptableObjectIdentity { Guid = "unit-test-guid" };
			var identity2 = new ScriptableObjectIdentity { Guid = "unit-test-guid" };

			identity1.Should().Be(identity2);
		}

		[Test]
		public void ShouldNotBeEqualIfTheyAreDifferentInstancesHavingDifferentGuid()
		{
			var identity1 = new ScriptableObjectIdentity { Guid = "unit-test-guid" };
			var identity2 = new ScriptableObjectIdentity { Guid = "different-unit-test-guid" };

			identity1.Should().NotBe(identity2);
		}

		[Test]
		public void ShouldBeEqualIfTheyAreTheSameInstanceComparedWithDoubleEqualOperator()
		{
			var identity = new ScriptableObjectIdentity { Guid = "unit-test-guid" };

#pragma warning disable CS1718
			// Justification: we want to test this here
			// ReSharper disable once EqualExpressionComparison
			(identity == identity).Should().BeTrue();
#pragma warning restore CS1718
		}

		[Test]
		public void ShouldBeEqualIfTheyAreTheDifferentInstancesWithSameGuidComparedWithDoubleEqualOperator()
		{
			var identity1 = new ScriptableObjectIdentity { Guid = "unit-test-guid" };
			var identity2 = new ScriptableObjectIdentity { Guid = "unit-test-guid" };

			(identity1 == identity2).Should().BeTrue();
		}

		[Test]
		public void ShouldNotBeEqualIfTheyAreTheDifferentInstancesWithDifferentGuidComparedWithDoubleEqualOperator()
		{
			var identity1 = new ScriptableObjectIdentity { Guid = "unit-test-guid" };
			var identity2 = new ScriptableObjectIdentity { Guid = "different-unit-test-guid" };

			(identity1 != identity2).Should().BeTrue();
		}
	}
}
