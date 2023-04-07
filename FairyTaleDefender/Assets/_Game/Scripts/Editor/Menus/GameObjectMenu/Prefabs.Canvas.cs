using Cysharp.Threading.Tasks;
using UnityEditor;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.UI + "/Canvas", priority = UIMenuPriority * 2)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateCanvasAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Canvas);
		}

		[MenuItem(Constants.MenuNames.GameObjectMenus.UI + "/WorldSpace Canvas", priority = UIMenuPriority * 2)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateWorldSpaceCanvasAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.WorldSpaceCanvas);
		}
	}
}
