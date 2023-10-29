using Cysharp.Threading.Tasks;
using UnityEditor;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.Texts + "/Text", priority = UIMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateTextAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Texts.Text);
		}

		[MenuItem(Constants.MenuNames.GameObjectMenus.Texts + "/Settings Text", priority = UIMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateSettingsTextAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Texts.SettingsText);
		}

		[MenuItem(Constants.MenuNames.GameObjectMenus.Texts + "/Text", true)]
		[MenuItem(Constants.MenuNames.GameObjectMenus.Texts + "/Settings Text", true)]
		private static bool TextValidation() => SelectionHasCanvasValidate();
	}
}
