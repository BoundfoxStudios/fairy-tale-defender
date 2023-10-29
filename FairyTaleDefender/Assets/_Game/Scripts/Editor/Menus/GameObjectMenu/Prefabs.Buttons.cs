using Cysharp.Threading.Tasks;
using UnityEditor;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.Buttons + "/Brown Button", priority = UIMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateBrownButtonAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Buttons.BrownButton);
		}

		[MenuItem(Constants.MenuNames.GameObjectMenus.Buttons + "/Toggle", priority = UIMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateToggleAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Buttons.Toggle);
		}

		[MenuItem(Constants.MenuNames.GameObjectMenus.Buttons + "/Dropdown", priority = UIMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateDropdownAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Buttons.Dropdown);
		}


		[MenuItem(Constants.MenuNames.GameObjectMenus.Buttons + "/Brown Button", true)]
		[MenuItem(Constants.MenuNames.GameObjectMenus.Buttons + "/Toggle", true)]
		[MenuItem(Constants.MenuNames.GameObjectMenus.Buttons + "/Dropdown", true)]
		private static bool ButtonValidation() => SelectionHasCanvasValidate();
	}
}
