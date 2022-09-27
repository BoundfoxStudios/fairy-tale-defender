using UnityEditor;

namespace BoundfoxStudios.CommunityProject.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.Texts + "/Text", priority = MenuPriority)]
		private static void CreateText()
		{
			SafeInstantiate(prefabManager => prefabManager.Texts.Text);
		}

		[MenuItem(Constants.MenuNames.GameObjectMenus.Texts + "/Text", true)]
		private static bool TextValidation() => SelectionHasCanvasValidate();
	}
}
