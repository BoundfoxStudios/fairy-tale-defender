using UnityEditor;

namespace BoundfoxStudios.CommunityProject.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.Cameras + "/Menu", priority = CamerasMenuPriority)]
		private static void CreateMenuCamera()
		{
			SafeInstantiate(prefabManager => prefabManager.Cameras.Menu);
		}

		[MenuItem(Constants.MenuNames.GameObjectMenus.Cameras + "/Level", priority = CamerasMenuPriority)]
		private static void CreateLevelCamera()
		{
			SafeInstantiate(prefabManager => prefabManager.Cameras.Level);
		}
	}
}
