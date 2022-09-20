using System.Collections;
using BoundfoxStudios.CommunityProject.Build.BuildManifest;
using Cysharp.Threading.Tasks;
using FluentAssertions;
using UnityEngine.TestTools;

namespace BoundfoxStudios.CommunityProject.Tests.Build.BuildManifest
{
	public class BuildManifestReaderTests
	{
		[UnityTest]
		public IEnumerator CanReadBuildManifest() => UniTask.ToCoroutine(async () =>
		{
			var sut = new BuildManifestReader();

			var buildManifest = await sut.LoadAsync();

			buildManifest.Should().NotBeNull();
			buildManifest.Sha.Should().Be("example-sha");
			buildManifest.ShortSha.Should().Be("short-sha");
			buildManifest.RunId.Should().Be(13371337);
			buildManifest.RunNumber.Should().Be(815);
		});
	}
}
