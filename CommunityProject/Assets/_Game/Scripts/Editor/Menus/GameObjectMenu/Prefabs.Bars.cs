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

		[MenuItem(Constants.MenuNames.GameObjectMenus.Bars + "/Bar", true)]
		private static bool BarValidation() => SelectionHasCanvasValidate();
	}
}
