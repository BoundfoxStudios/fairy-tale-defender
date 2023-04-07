using Cysharp.Threading.Tasks;
using UnityEditor;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.Editor + "/EditorColdStartup", priority = EditorMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateEditorColdStartupAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Editor.EditorColdStartup);
		}
	}
}
