using Cysharp.Threading.Tasks;
using UnityEditor;

namespace BoundfoxStudios.CommunityProject.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.Bars + "/Bar", priority = UIMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateBarAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Bars.Bar);
		}

		[MenuItem(Constants.MenuNames.GameObjectMenus.Bars + "/HealthBar", priority = UIMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateHealthBarAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Bars.HealthBar);
		}

		[MenuItem(Constants.MenuNames.GameObjectMenus.Bars + "/Bar", true)]
		[MenuItem(Constants.MenuNames.GameObjectMenus.Bars + "/HealthBar", true)]
		private static bool BarValidation() => SelectionHasCanvasValidate();
	}
}
