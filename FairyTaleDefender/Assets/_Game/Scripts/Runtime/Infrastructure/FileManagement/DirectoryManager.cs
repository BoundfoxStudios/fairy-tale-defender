using System;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.FileManagement
{
	public class DirectoryManager
	{
		private readonly string _rootPath = Application.persistentDataPath;

		/// <summary>
		/// Checks if the specified directory exists.
		/// </summary>
		/// <param name="directory">The directory to check.</param>
		public UniTask<bool> ExistsAsync(string directory)
		{
			var result = Directory.Exists(CreateDirectoryPath(directory));

			return UniTask.FromResult(result);
		}


		/// <summary>
		/// Lists all files in the given <paramref name="directory"/>.
		/// </summary>
		/// <param name="directory">The directory to list files.</param>
		/// <returns></returns>
		public UniTask<string[]> ListFilesAsync(string directory)
		{
			var result = Directory.EnumerateFiles(CreateDirectoryPath(directory));

			return UniTask.FromResult(result.ToArray());
		}

		/// <summary>
		/// Lists all directories within the given <paramref name="directory"/>.
		/// </summary>
		/// <param name="directory">The directory to list directories.</param>
		/// <returns></returns>
		public UniTask<string[]> ListDirectoriesAsync(string directory)
		{
			var path = CreateDirectoryPath(directory);

			if (!Directory.Exists(path))
			{
				return UniTask.FromResult(Array.Empty<string>());
			}

			var result = Directory.EnumerateDirectories(path);

			return UniTask.FromResult(result.ToArray());
		}

		/// <summary>
		/// Deletes a directory recursively.
		/// </summary>
		/// <param name="directory">The directory to delete.</param>
		/// <returns></returns>
		public UniTask DeleteAsync(string directory)
		{
			var path = CreateDirectoryPath(directory);

			if (!Directory.Exists(path))
			{
				return UniTask.CompletedTask;
			}

			Directory.Delete(path, true);

			return UniTask.CompletedTask;
		}

		private string CreateDirectoryPath(string directory) => Path.Combine(_rootPath, directory);
	}
}
