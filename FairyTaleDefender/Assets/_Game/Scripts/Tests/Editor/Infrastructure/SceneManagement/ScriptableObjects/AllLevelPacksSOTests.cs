using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Tests.Editor.Infrastructure.SceneManagement.ScriptableObjects
{
	public class AllLevelPacksSOTests
	{
		private AllLevelPacksSO _sut = default!;

		[SetUp]
		public void SetUp()
		{
			_sut = CreateSystemUnderTest();
		}

		private AllLevelPacksSO CreateSystemUnderTest()
		{
			var sut = ScriptableObject.CreateInstance<AllLevelPacksSO>();
			sut.LevelPacks = new LevelPackSO[2];

			var firstLevelPack = ScriptableObject.CreateInstance<LevelPackSO>();
			var firstPackFirstLevel = ScriptableObject.CreateInstance<LevelSO>();
			var firstPackSecondLevel = ScriptableObject.CreateInstance<LevelSO>();

			var secondLevelPack = ScriptableObject.CreateInstance<LevelPackSO>();
			var secondPackFirstLevel = ScriptableObject.CreateInstance<LevelSO>();

			sut.LevelPacks[0] = firstLevelPack;
			firstLevelPack.Levels = new[] { firstPackFirstLevel, firstPackSecondLevel };

			sut.LevelPacks[1] = secondLevelPack;
			secondLevelPack.Levels = new[] { secondPackFirstLevel };

			return sut;
		}

		[TearDown]
		public void TearDown()
		{
			_sut = null!;
		}

		[Test]
		public void LinkAllLevel_ShouldSetPreviousLevelPack_IfThereAreMore()
		{
			var expectedLevelPack = _sut.LevelPacks[0];

			_sut.LinkAllLevel();
			var result = _sut.LevelPacks[1].PreviousLevelPack;

			result.Should().Be(expectedLevelPack);
		}

		[Test]
		public void LinkAllLevel_ShouldSetNextLevelPack_IfThereAreMore()
		{
			var expectedLevelPack = _sut.LevelPacks[1];

			_sut.LinkAllLevel();
			var result = _sut.LevelPacks[0].NextLevelPack;

			result.Should().Be(expectedLevelPack);
		}

		[Test]
		public void LinkAllLevel_ShouldNotSetPreviousLevelPack_IfThereIsNone()
		{
			_sut.LinkAllLevel();

			var result = _sut.LevelPacks[0].PreviousLevelPack;
			result.Should().BeNull();
		}

		[Test]
		public void LinkAllLevel_ShouldNotSetNextLevelPack_IfThereAreNoMore()
		{
			_sut.LinkAllLevel();

			var nextLevelPack = _sut.LevelPacks[^1].NextLevelPack;
			nextLevelPack.Should().BeNull();
		}

		[Test]
		public void LinkAllLevel_ShouldSetNextLevel_IfThereAreMoreInSamePack()
		{
			var expectedLevel = _sut.LevelPacks[0].Levels[1];

			_sut.LinkAllLevel();

			var nextLevel = _sut.LevelPacks[0].Levels[0].NextLevel;
			nextLevel.Should().Be(expectedLevel);
		}

		[Test]
		public void LinkAllLevel_ShouldSetNextLevel_IfThereAreMoreInNextPack()
		{
			var expectedLevel = _sut.LevelPacks[1].Levels[0];

			_sut.LinkAllLevel();

			var result = _sut.LevelPacks[0].Levels[1].NextLevel;
			result.Should().Be(expectedLevel);
		}

		[Test]
		public void LinkAllLevel_ShouldNotSetNextLevel_IfThereAreNoMore()
		{
			_sut.LinkAllLevel();

			var result = _sut.LevelPacks[^1].Levels[^1].NextLevel;
			result.Should().BeNull();
		}

		[Test]
		public void LinkAllLevel_ShouldSetPreviousLevel_IfThereAreMoreInSamePack()
		{
			var expectedLevel = _sut.LevelPacks[0].Levels[0];

			_sut.LinkAllLevel();

			var result = _sut.LevelPacks[0].Levels[1].PreviousLevel;
			result.Should().Be(expectedLevel);
		}

		[Test]
		public void LinkAllLevel_ShouldSetPreviousLevel_IfThereAreMoreInPreviousPack()
		{
			var expectedLevel = _sut.LevelPacks[0].Levels[1];

			_sut.LinkAllLevel();

			var result = _sut.LevelPacks[1].Levels[0].PreviousLevel;
			result.Should().Be(expectedLevel);
		}

		[Test]
		public void LinkAllLevel_ShouldNotSetPreviousLevel_IfThereIsNone()
		{
			_sut.LinkAllLevel();

			var result = _sut.LevelPacks[0].Levels[0].PreviousLevel;
			result.Should().BeNull();
		}
	}
}
