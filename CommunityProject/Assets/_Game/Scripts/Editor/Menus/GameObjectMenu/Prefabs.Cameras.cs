using Cysharp.Threading.Tasks;
using UnityEditor;

namespace BoundfoxStudios.CommunityProject.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.Cameras + "/Menu", priority = CamerasMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateMenuCameraAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Cameras.Menu);
		}
	}
}
