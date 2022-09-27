using UnityEditor;

namespace BoundfoxStudios.CommunityProject.Editor.Menus
{
	public static class EditorPrefabManagerMenus
	{
		[MenuItem(Constants.MenuNames.MenuName + "/Select PrefabManager", priority = 0)]
		private static void PingPrefabManager()
		{
			PrefabManager.SafeInvoke(prefabManager => Selection.activeObject = prefabManager);
		}
	}
}
