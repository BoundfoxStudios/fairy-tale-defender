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


		[MenuItem(Constants.MenuNames.GameObjectMenus.Buttons + "/Brown Button", true)]
		private static bool ButtonValidation() => SelectionHasCanvasValidate();
	}
}
