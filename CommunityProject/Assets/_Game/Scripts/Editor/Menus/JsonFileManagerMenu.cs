using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Editor.Menus
{
	public class JsonFileManagerMenu
	{
		[MenuItem("Community Project/Open Application Data")]
		private static void OpenApplicationData()
		{
			EditorUtility.RevealInFinder(Application.persistentDataPath);
		}
	}
}
