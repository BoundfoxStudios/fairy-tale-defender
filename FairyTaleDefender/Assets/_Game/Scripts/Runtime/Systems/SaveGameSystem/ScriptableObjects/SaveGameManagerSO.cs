using System;
using System.Collections.Generic;
using System.IO;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.FileManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.SaveGameSystem.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.SaveGame + "/Save Game Manager")]
	public class SaveGameManagerSO : ScriptableObject
	{
		private DirectoryManager _directoryManager = default!;
		private JsonFileManager _fileManager = default!;

		private void OnEnable()
		{
			_directoryManager = new();
			_fileManager = new();
		}

		public async UniTask<List<SaveGameMeta>> ListAvailableSaveGamesAsync()
		{
			var directories = await _directoryManager.ListDirectoriesAsync(Constants.SaveGames.DirectoryName);

			var candidates = new List<string>();

			foreach (var directory in directories)
			{
				if (await IsValidSaveGameAsync(directory))
				{
					candidates.Add(directory);
				}
			}

			var metas = new List<SaveGameMeta>();

			foreach (var candidate in candidates)
			{
				var meta = await _fileManager.ReadAsync<SaveGameMeta>(CreatePath(candidate, Constants.SaveGames.MetaFileName));
				meta.Directory = candidate;

				metas.Add(meta);
			}

			return metas;
		}

		public async UniTask<SaveGame?> LoadSaveGameAsync(SaveGameMeta meta)
		{
			if (!await IsValidSaveGameAsync(meta.Directory))
			{
				return null;
			}

			var saveGameData = await _fileManager.ReadAsync<SaveGameData>(meta.DataFilePath);

			var saveGame = new SaveGame(meta, saveGameData);

			return saveGame;
		}

		public async UniTask<SaveGame?> CreateSaveGameAsync(string saveName, SaveGameData data)
		{
			var slugifiedName = saveName.Slugify();

			var meta = new SaveGameMeta
			{
				Name = saveName,
				LastPlayedDate = DateTime.Now,
				Directory = CreatePath(slugifiedName),
			};

			try
			{
				var saveGameFilePath = await _fileManager.WriteAsync(meta.DataFilePath, data);

				meta.Hash = SaveGameHash.Create(meta.Name, saveGameFilePath);

				await _fileManager.WriteAsync(meta.MetaFilePath, meta);
			}

			catch
			{
				await _directoryManager.DeleteAsync(meta.Directory);
				return null;
			}

			return new(meta, data);
		}

		public async UniTaskVoid SaveGameAsync(SaveGame saveGame)
		{
			saveGame.Meta.LastPlayedDate = DateTime.Now;

			var saveGameFilePath = await _fileManager.WriteAsync(saveGame.DataFilePath, saveGame.Data);

			saveGame.Meta.Hash = SaveGameHash.Create(saveGame.Meta.Name, saveGameFilePath);

			await _fileManager.WriteAsync(saveGame.MetaFilePath, saveGame.Meta);
		}

		public async UniTask<bool> SaveGameExistsAsync(string saveName)
		{
			var slugifiedName = saveName.Slugify();

			return await IsValidSaveGameAsync(slugifiedName);
		}

		private async UniTask<bool> IsValidSaveGameAsync(string directory)
		{
			var saveGameFile = CreatePath(directory, Constants.SaveGames.SaveGameFileName);

			var metaFileExists = await _fileManager.ExistsAsync(CreatePath(directory, Constants.SaveGames.MetaFileName));
			var saveGameFileExists =
				await _fileManager.ExistsAsync(saveGameFile);

			var filesExist = metaFileExists && saveGameFileExists;

			if (!filesExist)
			{
				return false;
			}

			var meta = await LoadMeta(directory);
			var hash = SaveGameHash.Create(meta.Name, _fileManager.CreatePath(saveGameFile));

			return meta.Hash == hash;
		}

		private async UniTask<SaveGameMeta> LoadMeta(string directory) =>
			await _fileManager.ReadAsync<SaveGameMeta>(CreatePath(directory,
				Constants.SaveGames.MetaFileName));

		private string CreatePath(string directory, string fileName) => Path.Combine(CreatePath(directory), fileName);

		private string CreatePath(string directory) => Path.Combine(Constants.SaveGames.DirectoryName, directory);
	}
}
