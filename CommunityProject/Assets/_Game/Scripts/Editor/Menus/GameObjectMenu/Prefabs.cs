using System;
using BoundfoxStudios.CommunityProject.EditorExtensions.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Editor.Menus.GameObjectMenu
{
	public static partial class Prefabs
	{
		private const int MenuPriority = -50;
		private const int Separator = 11;

		private const int CamerasMenuPriority = MenuPriority;
		private const int UIMenuPriority = CamerasMenuPriority + Separator;
		private const int EditorMenuPriority = UIMenuPriority + Separator;

		private static bool SelectionHasCanvasValidate() =>
			Selection.activeGameObject && Selection.activeGameObject.GetComponentInParent<Canvas>();

		private static void SafeInstantiate(Func<PrefabManagerSO, GameObject> itemSelector)
		{
			PrefabManager.SafeInvoke(prefabManager =>
			{
				var item = itemSelector(prefabManager);

				if (!item)
				{
					Debug.LogWarning($"{nameof(SafeInstantiate)} invoked, but {nameof(itemSelector)} returned null. " +
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
