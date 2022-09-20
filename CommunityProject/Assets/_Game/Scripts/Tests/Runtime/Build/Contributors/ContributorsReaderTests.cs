using System.Collections;
using BoundfoxStudios.CommunityProject.Build.Contributors;
using Cysharp.Threading.Tasks;
using FluentAssertions;
using UnityEngine.TestTools;

namespace BoundfoxStudios.CommunityProject.Tests.Build.Contributors
{
	public class ContributorsReaderTests
	{
		[UnityTest]
		public IEnumerator CanReadContributors() => UniTask.ToCoroutine(async () =>
		{
			var sut = new ContributorsReader();

			var contributors = await sut.LoadAsync();

			contributors.Should().NotBeNull();
			contributors.Should().HaveCount(1);

			var contributor = contributors[0];

			contributor.User.Should().Be("ThisIsAUnitTestUserThatWillBeOverwrittenLater");
			contributor.Url.Should().Be("https://github.com/ThisIsAUnitTestUserThatWillBeOverwrittenLater");
			contributor.Contributions.Should().Be(99);
		});
	}
}
