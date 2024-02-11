using System.Collections;
using BoundfoxStudios.FairyTaleDefender.Build.BuildManifest;
using Cysharp.Threading.Tasks;
using FluentAssertions;
using UnityEngine.TestTools;

namespace BoundfoxStudios.FairyTaleDefender.Tests.Build.BuildManifest
{
	public class BuildManifestReaderTests
	{
		[UnityTest]
		public IEnumerator LoadAsync_CanReadBuildManifest() => UniTask.ToCoroutine(async () =>
		{
			var sut = new BuildManifestReader();

			var buildManifest = await sut.LoadAsync();

			buildManifest.Should().NotBeNull();
			buildManifest.Sha.Should().Be("example-sha");
			buildManifest.ShortSha.Should().Be("example-");
		});
	}
}
