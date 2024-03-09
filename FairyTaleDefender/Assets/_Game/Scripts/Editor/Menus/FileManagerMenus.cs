using System.IO;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.FileManagement;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Menus
{
	public static class FileManagerMenus
	{
		[MenuItem(Constants.MenuNames.Files + "/Open Application Data", priority = 100)]
		private static void OpenApplicationData()
		{
			EditorUtility.RevealInFinder(Application.persistentDataPath);
		}

		[MenuItem(Constants.MenuNames.Files + "/Delete Default Save Game", priority = 1000)]
		private static void DeleteDefaultSaveGame()
		{
			DeleteDefaultSaveGameAsync().Forget();
		}

		private static async UniTaskVoid DeleteDefaultSaveGameAsync()
		{
			var directoryManager = new DirectoryManager();
			var path = Path.Combine(Common.Constants.SaveGames.DirectoryName, Common.Constants.SaveGames.DefaultSaveGameName);

			if (!await directoryManager.ExistsAsync(path))
			{
				Debug.Log($"Path \"{path}\" does not exist. Did you already save a game?");
				return;
			}

			await directoryManager.DeleteAsync(path);
			Debug.Log("Default Save Game has been deleted!");
		}


		[MenuItem(Constants.MenuNames.Files + "/Delete All Save Games", priority = 1001)]
		private static void DeleteAllSaveGames()
		{
			DeleteAllSaveGamesAsync().Forget();
		}

		private static async UniTaskVoid DeleteAllSaveGamesAsync()
		{
			var directoryManager = new DirectoryManager();
			var path = Common.Constants.SaveGames.DirectoryName;

			if (!await directoryManager.ExistsAsync(path))
			{
				Debug.Log($"Path \"{path}\" does not exist. Did you already save a game?");
				return;
			}

			var confirmDeletion = EditorUtility.DisplayDialog("Confirm deletion",
				"Do you really want to delete all save games?",
				"Yes, delete all", "No!");

			if (!confirmDeletion)
			{
				return;
			}

			await directoryManager.DeleteAsync(path);
			Debug.Log("All Save Games have been deleted!");
		}
	}
}
