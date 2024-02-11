using System.IO;
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
			var path = Path.Combine(Application.persistentDataPath, Common.Constants.SaveGames.DirectoryName,
				Common.Constants.SaveGames.DefaultSaveGameName);

			if (!Directory.Exists(path))
			{
				Debug.Log($"Path \"{path}\" does not exist. Did you already save a game?");
				return;
			}

			Directory.Delete(path, true);
			Debug.Log("Default Save Game has been deleted!");
		}

		[MenuItem(Constants.MenuNames.Files + "/Delete All Save Games", priority = 1001)]
		private static void DeleteAllSaveGames()
		{
			var path = Path.Combine(Application.persistentDataPath, Common.Constants.SaveGames.DirectoryName);

			if (!Directory.Exists(path))
			{
				Debug.Log($"Path \"{path}\" does not exist. Did you already save a game?");
				return;
			}

			var confirmDeletion = EditorUtility.DisplayDialog("Confirm deletion",
				"Do you really want to delete all save games?", "Yes, delete all",
				"No!");

			if (!confirmDeletion)
			{
				return;
			}

			Directory.Delete(path, true);
			Debug.Log("All Save Games have been deleted!");
		}
	}
}
