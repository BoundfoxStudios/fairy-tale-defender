using System.Collections;
using BoundfoxStudios.FairyTaleDefender.Build.Contributors;
using Cysharp.Threading.Tasks;
using FluentAssertions;
using UnityEngine.TestTools;

namespace BoundfoxStudios.FairyTaleDefender.Tests.Build.Contributors
{
	public class ContributorsReaderTests
	{
		[UnityTest]
		public IEnumerator LoadAsync_CanReadContributors() => UniTask.ToCoroutine(async () =>
		{
			var sut = new ContributorsReader();

			var contributors = await sut.LoadAsync();

			contributors.Should().NotBeNull();
			contributors.Should().HaveCount(2);

			var contributor = contributors[0];

			contributor.User.Should().Be("Unit Test Account");
			contributor.GitHubAccount.Should().Be("unitTestAccount");
			contributor.ProfileUrl.Should().Be("https://github.com/unitTestAccount");
			contributor.Contributions.Should().HaveCount(5);
			contributor.Contributions[0].Should().Be("doc");
			contributor.Contributions[1].Should().Be("code");
			contributor.Contributions[2].Should().Be("ideas");
			contributor.Contributions[3].Should().Be("design");
			contributor.Contributions[4].Should().Be("audio");
		});
	}
}
