using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Editor.Menus
{
	public static class FileManagerMenus
	{
		[MenuItem(Constants.MenuNames.MenuName + "/Open Application Data")]
		private static void OpenApplicationData()
		{
			EditorUtility.RevealInFinder(Application.persistentDataPath);
		}
	}
}
