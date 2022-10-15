using System;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;


namespace BoundfoxStudios.CommunityProject.Infrastructure.FileManager
{
	public class JsonFileManager : IFileManager
	{
		private string _rootPath = Application.persistentDataPath;

		[MenuItem("Community Project/Open Application Data")]
		static void OpenApplicationData()
		{
			EditorUtility.RevealInFinder(Application.persistentDataPath);
		}


		public UniTask<bool> ExistsAsync(string key)
		{
			var filePath = Path.Combine(_rootPath, key);

			var result = File.Exists(filePath);

			return UniTask.FromResult(result);
		}

		public UniTask WriteAsync<T>(string key, T serializable)
		{
			var jsonSerialization = JsonUtility.ToJson(serializable);
			var filePath = Path.Combine(_rootPath, key);

			File.WriteAllText(filePath, jsonSerialization);

			return UniTask.CompletedTask;
		}

		public UniTask<T> ReadAsync<T>(string key)
		{
			var filePath = Path.Combine(_rootPath, key);
			var jsonFromFile = File.ReadAllText(filePath);

			var result = JsonUtility.FromJson<T>(jsonFromFile);

			return UniTask.FromResult(result);
		}
	}
}
