using UnityEditor;

namespace BoundfoxStudios.CommunityProject.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.UI + "/Canvas", priority = UIMenuPriority * 2)]
		private static void CreateCanvas()
		{
			SafeInstantiate(prefabManager => prefabManager.Canvas);
		}
	}
}
