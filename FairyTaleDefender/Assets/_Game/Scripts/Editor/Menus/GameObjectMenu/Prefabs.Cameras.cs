using Cysharp.Threading.Tasks;
using UnityEditor;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.Cameras + "/Menu", priority = CamerasMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateMenuCameraAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Cameras.Menu);
		}

		[MenuItem(Constants.MenuNames.GameObjectMenus.Cameras + "/Level", priority = CamerasMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateLevelCameraAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Cameras.Level);
		}
	}
}
