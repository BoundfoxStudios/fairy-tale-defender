using Cysharp.Threading.Tasks;
using UnityEditor;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Menus
{
	public static class EditorPrefabManagerMenus
	{
		[MenuItem(Constants.MenuNames.MenuName + "/Select PrefabManager", priority = 0)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid PingPrefabManagerAsync()
		{
			await PrefabManager.SafeInvokeAsync(prefabManager => Selection.activeObject = prefabManager);
		}
	}
}
