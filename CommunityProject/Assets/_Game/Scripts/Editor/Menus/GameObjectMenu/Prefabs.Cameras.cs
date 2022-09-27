using UnityEditor;

namespace BoundfoxStudios.CommunityProject.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.Lights + "/Sun", priority = LightsMenuPriority)]
		private static void CreateSun()
		{
			SafeInstantiate(prefabManager => prefabManager.Lights.Sun);
		}
	}
}
