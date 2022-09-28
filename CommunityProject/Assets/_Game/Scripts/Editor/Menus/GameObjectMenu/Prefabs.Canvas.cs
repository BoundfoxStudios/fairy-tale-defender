using Cysharp.Threading.Tasks;
using UnityEditor;

namespace BoundfoxStudios.CommunityProject.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.UI + "/Canvas", priority = UIMenuPriority * 2)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateCanvasAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Canvas);
		}
	}
}
