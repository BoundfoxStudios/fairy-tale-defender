using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Infrastructure.FileManagement
{
	public class JsonFileManager : IFileManager
	{
		private string _rootPath = Application.persistentDataPath;

		public UniTask<bool> ExistsAsync(string key)
		{
			var result = File.Exists(CreateFilePath(key));

			return UniTask.FromResult(result);
		}

		public UniTask WriteAsync<T>(string key, T serializable)
		{
			var jsonSerialization = JsonUtility.ToJson(serializable);

			var path = CreateFilePath(key);
			EnsurePath(path);

			File.WriteAllText(path, jsonSerialization);

			return UniTask.CompletedTask;
		}

		public UniTask<T> ReadAsync<T>(string key)
		{
			var jsonFromFile = File.ReadAllText(CreateFilePath(key));

			var result = JsonUtility.FromJson<T>(jsonFromFile);

			return UniTask.FromResult(result);
		}

		private string CreateFilePath(string key)
		{
			return Path.Combine(_rootPath, key);
		}

		private void EnsurePath(string path)
		{
			var folderPath = Path.GetDirectoryName(path);
			if (!Directory.Exists(folderPath) && folderPath != null)
			{
				Directory.CreateDirectory(folderPath);
			}
		}
	}
}
