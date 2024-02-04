using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.FileManagement
{
	public class JsonFileManager
	{
		private readonly string _rootPath = Application.persistentDataPath;

		public UniTask<bool> ExistsAsync(string key)
		{
			var result = File.Exists(CreatePath(key));

			return UniTask.FromResult(result);
		}

		public async UniTask<string> WriteAsync<T>(string key, T serializable)
		{
			var jsonSerialization = JsonUtility.ToJson(serializable);

			var path = CreatePath(key);
			EnsurePath(path);

			await File.WriteAllTextAsync(path, jsonSerialization);

			return path;
		}

		public async UniTask<T> ReadAsync<T>(string key)
		{
			var jsonFromFile = await File.ReadAllTextAsync(CreatePath(key));

			return JsonUtility.FromJson<T>(jsonFromFile);
		}

		public string CreatePath(string key) => Path.Combine(_rootPath, key);

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
