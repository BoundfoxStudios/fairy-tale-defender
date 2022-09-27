using UnityEditor;

namespace BoundfoxStudios.CommunityProject.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.Editor + "/EditorColdStartup", priority = MenuPriority)]
		private static void CreateEditorColdStartup()
		{
			SafeInstantiate(prefabManager => prefabManager.Editor.EditorColdStartup);
		}
	}
}
