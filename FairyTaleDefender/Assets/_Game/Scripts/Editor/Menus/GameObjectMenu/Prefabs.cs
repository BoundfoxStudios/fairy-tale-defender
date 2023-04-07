using System;
using BoundfoxStudios.FairyTaleDefender.EditorExtensions.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		private const int MenuPriority = -50;
		private const int Separator = 11;

		private const int CamerasMenuPriority = MenuPriority;
		private const int LightsMenuPriority = CamerasMenuPriority + 1;
		private const int UIMenuPriority = LightsMenuPriority + Separator;
		private const int EditorMenuPriority = UIMenuPriority + Separator;

		private static bool SelectionHasCanvasValidate() =>
			Selection.activeGameObject && Selection.activeGameObject.GetComponentInParent<Canvas>();

		private static async UniTask SafeInstantiateAsync(Func<PrefabManagerSO, GameObject?> itemSelector)
		{
			await PrefabManager.SafeInvokeAsync(prefabManager =>
			{
				var item = itemSelector(prefabManager);

				if (item is null)
				{
					Debug.LogWarning($"{nameof(SafeInstantiateAsync)} invoked, but {nameof(itemSelector)} returned null. " +
									 "Did you forget to fill the slot in the inspector?");
					return;
				}

				var instance = PrefabUtility.InstantiatePrefab(item, Selection.activeTransform);

				Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
				Selection.activeObject = instance;
			});
		}
	}
}
