using UnityEditor;

namespace BoundfoxStudios.CommunityProject.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.UI + "/Canvas", priority = MenuPriority)]
		private static void CreateCanvas()
		{
			SafeInstantiate(prefabManager => prefabManager.Canvas);
		}
	}
}
