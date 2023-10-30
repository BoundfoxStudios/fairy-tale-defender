using BoundfoxStudios.FairyTaleDefender.UI.Utility;
using Cysharp.Threading.Tasks;
using UnityEditor;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.Containers + "/Tab Group", priority = UIMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateTabGroupAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Containers.TabGroup);
		}

		[MenuItem(Constants.MenuNames.GameObjectMenus.Containers + "/Tab Group Header Button", priority = UIMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateTabGroupHeaderButtonAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Containers.TabGroupHeaderButton);
		}


		[MenuItem(Constants.MenuNames.GameObjectMenus.Buttons + "/Tab Group", true)]
		private static bool ContainersValidation() => SelectionHasCanvasValidate();

		[MenuItem(Constants.MenuNames.GameObjectMenus.Containers + "/Tab Group Header Button", true)]
		private static bool TabGroupHeaderButtonValidation() => SelectionHasCanvasValidate() &&
																Selection.activeGameObject.GetComponentInParent<TabGroup>() &&
																Selection.activeGameObject.GetComponentInParent<ToggleButtonGroup>();
	}
}
