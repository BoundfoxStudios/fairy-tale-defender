using System;
using BoundfoxStudios.CommunityProject.EditorExtensions.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Editor
{
	public static class PrefabManager
	{
		public static PrefabManagerSO _prefabManager;

		private static PrefabManagerSO LocatePrefabManager()
		{
			// Unfortunately, we can not use the Addressables here to locate the PrefabManagerSO instance, because
			// Addressables do not work well in editor scripts due to their async nature
			// which does not work well in-editor, yet.
			var prefabManagerGuids =
				AssetDatabase.FindAssets($"t:{nameof(PrefabManagerSO)}", new[] { "Assets/_Game/ScriptableObjects" });

			if (prefabManagerGuids.Length != 1)
			{
				Debug.LogError(
					$"Expected exactly 1 instance of {nameof(PrefabManagerSO)} but found {prefabManagerGuids.Length}");
				return null;
			}

			var path = AssetDatabase.GUIDToAssetPath(prefabManagerGuids[0]);

			return AssetDatabase.LoadAssetAtPath<PrefabManagerSO>(path);
		}

		public static void SafeInvoke(Action<PrefabManagerSO> callback)
		{
			if (!_prefabManager)
			{
				var prefabManager = LocatePrefabManager();

				if (!prefabManager)
				{
					return;
				}

				_prefabManager = prefabManager;
			}

			callback(_prefabManager);
		}
	}
}
