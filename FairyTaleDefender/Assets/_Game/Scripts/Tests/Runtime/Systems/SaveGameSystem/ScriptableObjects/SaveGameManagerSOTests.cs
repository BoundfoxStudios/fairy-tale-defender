using System;
using System.Collections;
using System.IO;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.FileManagement;
using BoundfoxStudios.FairyTaleDefender.Systems.SaveGameSystem;
using BoundfoxStudios.FairyTaleDefender.Systems.SaveGameSystem.ScriptableObjects;
using Cysharp.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BoundfoxStudios.FairyTaleDefender.Tests.Systems.SaveGameSystem.ScriptableObjects
{
	public class SaveGameManagerSOTests
	{
		private const string BackupDirectoryName = "Save Games Backup";

		private string SaveGamesPath => Path.Combine(Application.persistentDataPath, Constants.SaveGames.DirectoryName);
		private string SaveGamesBackupPath => Path.Combine(Application.persistentDataPath, BackupDirectoryName);

		[SetUp]
		public void BackupSaveGamesFolder()
		{
			if (Directory.Exists(SaveGamesPath))
			{
				Directory.Move(SaveGamesPath, SaveGamesBackupPath);
			}
		}

		[TearDown]
		public void RestoreSaveGamesFolderBackup()
		{
			if (Directory.Exists(SaveGamesBackupPath))
			{
				if (Directory.Exists(SaveGamesPath))
				{
					Directory.Delete(SaveGamesPath, true);
				}

				Directory.Move(SaveGamesBackupPath, SaveGamesPath);
			}
		}

		private async UniTask<SaveGameMeta> PrepareSaveGameAsync(bool isValid = true, string name = "1")
		{
			var fileManager = new JsonFileManager();
			name = name.Slugify();

			var saveGamePath = Path.Combine(SaveGamesPath, name);
			var metaPath = Path.Combine(saveGamePath, Constants.SaveGames.MetaFileName);

			var meta = new SaveGameMeta()
			{
				Name = $"Unit Test Save {name}",
				LastPlayedDate = DateTime.Now.Subtract(new TimeSpan(1)),
				Directory = saveGamePath,
			};

			await fileManager.WriteAsync(metaPath, meta);

			if (!isValid)
			{
				return meta;
			}

			var saveGameFilePath = Path.Combine(saveGamePath, Constants.SaveGames.SaveGameFileName);
			await fileManager.WriteAsync(saveGameFilePath, new SaveGameData()
			{
				LastLevel = null
			});

			meta.Hash = SaveGameHash.Create(meta.Name, saveGameFilePath);
			await fileManager.WriteAsync(metaPath, meta);

			return meta;
		}

		[UnityTest]
		public IEnumerator LoadAvailableSaveGamesAsync_CanListAvailableValidSaveGames() => UniTask.ToCoroutine(async () =>
		{
			await PrepareSaveGameAsync();

			var sut = ScriptableObject.CreateInstance<SaveGameManagerSO>();

			var metas = await sut.ListAvailableSaveGamesAsync();

			metas.Count.Should().Be(1);
			metas[0].Name.Should().Be("Unit Test Save 1");
		});

		[UnityTest]
		public IEnumerator LoadAvailableSaveGamesAsync_CanListAvailableValidSaveGames_SkippingInvalidSaveGames() =>
			UniTask.ToCoroutine(async () =>
			{
				await PrepareSaveGameAsync(isValid: false);
				await PrepareSaveGameAsync(name: "2");

				var sut = ScriptableObject.CreateInstance<SaveGameManagerSO>();

				var metas = await sut.ListAvailableSaveGamesAsync();

				metas.Count.Should().Be(1);
				metas[0].Name.Should().Be("Unit Test Save 2");
			});

		[UnityTest]
		public IEnumerator LoadSaveGameAsync_ReturnsNull_WhenSaveGameIsInvalid() =>
			UniTask.ToCoroutine(async () =>
			{
				var meta = await PrepareSaveGameAsync(isValid: false);

				var sut = ScriptableObject.CreateInstance<SaveGameManagerSO>();

				var saveGame = await sut.LoadSaveGameAsync(meta);

				saveGame.Should().BeNull();
			});

		[UnityTest]
		public IEnumerator LoadSaveGameAsync_ReturnsASaveGame() =>
			UniTask.ToCoroutine(async () =>
			{
				var meta = await PrepareSaveGameAsync();

				var sut = ScriptableObject.CreateInstance<SaveGameManagerSO>();

				var saveGame = (await sut.LoadSaveGameAsync(meta))!;

				saveGame.Should().NotBeNull();
				saveGame.Meta.Should().NotBeNull();
				saveGame.Data.Should().NotBeNull();
			});

		[UnityTest]
		public IEnumerator CreateSaveGameAsync_CreatesASaveGame() =>
			UniTask.ToCoroutine(async () =>
			{
				var sut = ScriptableObject.CreateInstance<SaveGameManagerSO>();

				var meta = await sut.CreateSaveGameAsync("UnitTest", new());

				meta.Should().NotBeNull();
			});

		[UnityTest]
		public IEnumerator SaveGameExistsAsync_ReturnsFalse_WhenNoSaveGameExists() =>
			UniTask.ToCoroutine(async () =>
			{
				var sut = ScriptableObject.CreateInstance<SaveGameManagerSO>();

				var result = await sut.SaveGameExistsAsync("Non existing save game");

				result.Should().BeFalse();
			});

		[UnityTest]
		public IEnumerator SaveGameExistsAsync_ReturnsTrue() =>
			UniTask.ToCoroutine(async () =>
			{
				await PrepareSaveGameAsync(name: "Unit Test");

				var sut = ScriptableObject.CreateInstance<SaveGameManagerSO>();

				var result = await sut.SaveGameExistsAsync("Unit Test");

				result.Should().BeTrue();
			});

		[UnityTest]
		public IEnumerator LoadSaveGame_CannotLoadWhenFileWasTemperedWith() =>
			UniTask.ToCoroutine(async () =>
			{
				var meta = await PrepareSaveGameAsync(name: "Unit Test");

				// Temper with the file
				await File.AppendAllLinesAsync(Path.Combine(meta.Directory, Constants.SaveGames.SaveGameFileName), new []{" "});

				var sut = ScriptableObject.CreateInstance<SaveGameManagerSO>();

				var saveGame = await sut.LoadSaveGameAsync(meta);

				saveGame.Should().BeNull();
			});


	}
}
