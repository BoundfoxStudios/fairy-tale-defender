using Cysharp.Threading.Tasks;
using UnityEditor;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.Inputs + "/Input Field", priority = UIMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateInputFieldAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Inputs.InputField);
		}

		[MenuItem(Constants.MenuNames.GameObjectMenus.Inputs + "/Toggle", priority = UIMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateToggleAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Inputs.Toggle);
		}

		[MenuItem(Constants.MenuNames.GameObjectMenus.Inputs + "/Dropdown", priority = UIMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateDropdownAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Inputs.Dropdown);
		}


		[MenuItem(Constants.MenuNames.GameObjectMenus.Inputs + "/Toggle", true)]
		[MenuItem(Constants.MenuNames.GameObjectMenus.Inputs + "/Dropdown", true)]
		[MenuItem(Constants.MenuNames.GameObjectMenus.Inputs + "/Input Field", true)]
		private static bool InputValidation() => SelectionHasCanvasValidate();
	}
}
