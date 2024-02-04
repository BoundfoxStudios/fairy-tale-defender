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

			var saveGameData =
				await _fileManager.ReadAsync<SaveGameData>(CreatePath(meta.Directory, Constants.SaveGames.SaveGameFileName));

			var saveGame = new SaveGame(meta, saveGameData);

			return saveGame;
		}

		public async UniTask<SaveGameMeta?> CreateSaveGameAsync(string saveName, SaveGameData data)
		{
			var slugifiedName = saveName.Slugify();

			var meta = new SaveGameMeta()
			{
				Name = saveName,
				LastPlayedDate = DateTime.Now,
				Directory = CreatePath(slugifiedName),
			};

			try
			{
				await _fileManager.WriteAsync(Path.Combine(meta.Directory, Constants.SaveGames.MetaFileName), meta);
				await _fileManager.WriteAsync(Path.Combine(meta.Directory, Constants.SaveGames.SaveGameFileName), data);
			}

			catch
			{
				await _directoryManager.DeleteAsync(meta.Directory);
				return null;
			}

			return meta;
		}

		public async UniTask<bool> SaveGameExistsAsync(string saveName)
		{
			var slugifiedName = saveName.Slugify();

			return await IsValidSaveGameAsync(slugifiedName);
		}

		private async UniTask<bool> IsValidSaveGameAsync(string directory)
		{
			var metaFileExists = await _fileManager.ExistsAsync(CreatePath(directory, Constants.SaveGames.MetaFileName));
			var saveGameFileExists =
				await _fileManager.ExistsAsync(CreatePath(directory, Constants.SaveGames.SaveGameFileName));

			return metaFileExists && saveGameFileExists;
		}

		private string CreatePath(string directory, string fileName) => Path.Combine(CreatePath(directory), fileName);

		private string CreatePath(string directory) => Path.Combine(Constants.SaveGames.DirectoryName, directory);
	}
}
