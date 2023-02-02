using Cysharp.Threading.Tasks;
using UnityEditor;

namespace BoundfoxStudios.CommunityProject.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		[MenuItem(Constants.MenuNames.GameObjectMenus.Lights + "/Sun", priority = LightsMenuPriority)]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private static async UniTaskVoid CreateSunAsync()
		{
			await SafeInstantiateAsync(prefabManager => prefabManager.Lights.Sun);
		}
	}
}
