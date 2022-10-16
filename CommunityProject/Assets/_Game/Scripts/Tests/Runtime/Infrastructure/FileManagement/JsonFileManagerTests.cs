using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using BoundfoxStudios.CommunityProject.Infrastructure.FileManagement;
using Cysharp.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BoundfoxStudios.CommunityProject.Tests.Infrastructure.FileManagement
{
	/// <summary>
	///   Contains Integration Tests
	/// </summary>
	public class JsonFileManagerTests
	{
		private readonly string _basePath = Application.persistentDataPath;
		private string UnitTestBaseFolder => Path.Combine(_basePath, "unit-test");
		private string UnitTestFile => Path.Combine(UnitTestBaseFolder, "unit.test");

		[OneTimeTearDown]
		public void CleanUp()
		{
			if (Directory.Exists(UnitTestBaseFolder))
			{
				Directory.Delete(UnitTestBaseFolder, true);
			}
		}

		[UnityTest]
		public IEnumerator ExistsAsync_ReturnsTrue_WhenFileExists() => UniTask.ToCoroutine(async () =>
		{
			Directory.CreateDirectory(UnitTestBaseFolder);
			await File.WriteAllTextAsync(UnitTestFile, "unit-test");

			var sut = new JsonFileManager();

			var result = await sut.ExistsAsync(UnitTestFile);

			result.Should().BeTrue();

			CleanUp();
		});

		[UnityTest]
		public IEnumerator ExistsAsync_ReturnsFalse_WhenFileDoesNotExist() => UniTask.ToCoroutine(async () =>
		{
			CleanUp();

			var sut = new JsonFileManager();

			var result = await sut.ExistsAsync(UnitTestFile);

			result.Should().BeFalse();
		});

		[UnityTest]
		public IEnumerator WriteAsync_WritesToFile() => UniTask.ToCoroutine(async () =>
		{
			var testObject = new UnitTestSerializable
			{
				Property = "unit-test-1337"
			};

			var sut = new JsonFileManager();
			await sut.WriteAsync(UnitTestFile, testObject);

			var fileContent = await File.ReadAllTextAsync(UnitTestFile);

			fileContent.Should().Contain("Property");
			fileContent.Should().Contain("unit-test-1337");

			CleanUp();
		});

		[UnityTest]
		public IEnumerator ReadAsync_CanDeserializeAFile() => UniTask.ToCoroutine(async () =>
		{
			var fileContent = @"
{
	""Property"": ""unit-test-1337""
}
";

			Directory.CreateDirectory(UnitTestBaseFolder);
			await File.WriteAllTextAsync(UnitTestFile, fileContent);

			var sut = new JsonFileManager();

			var result = await sut.ReadAsync<UnitTestSerializable>(UnitTestFile);

			result.Should().NotBeNull();
			result.Property.Should().Be("unit-test-1337");

			CleanUp();
		});

		[UnityTest]
		public IEnumerator ReadAsync_Throws_ThenFileDoesNotExist() => UniTask.ToCoroutine(async () =>
		{
			CleanUp();

			var sut = new JsonFileManager();

			Func<Task> action =() =>  sut.ReadAsync<UnitTestSerializable>(UnitTestFile).AsTask();

			await action.Should().ThrowAsync<Exception>();
		});

		[Serializable]
		private class UnitTestSerializable
		{
			public string Property;
		}
	}
}
