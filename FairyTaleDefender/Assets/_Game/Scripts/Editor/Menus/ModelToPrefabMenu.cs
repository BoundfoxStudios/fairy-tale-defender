using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Menus
{
	public static class ModelToPrefabMenu
	{
		private static readonly AssetLocator<GameObject> EnvironmentBaseAssetLocator =
			new("Prefabs/Environment/EnvironmentAsset_Base.prefab");
		private static readonly AssetLocator<GameObject> TileBaseAssetLocator =
			new("Prefabs/Environment/Tiles/Tile_Base.prefab");
		private static readonly AssetLocator<GameObject> TileWayBaseBaseAssetLocator =
			new("Prefabs/Environment/Tiles/Tile_Way_Base.prefab");

		[MenuItem("Assets/" + Constants.MenuNames.MenuName + "/Create Environment Prefab", priority = 20)]
		private static void ConvertToEnvironmentPrefab() => ConvertToPrefab(EnvironmentBaseAssetLocator);

		[MenuItem("Assets/" + Constants.MenuNames.MenuName + "/Create Tile Prefab", priority = 21)]
		private static void ConvertToTilePrefab() => ConvertToPrefab(TileBaseAssetLocator);

		[MenuItem("Assets/" + Constants.MenuNames.MenuName + "/Create Tile Way Prefab", priority = 22)]
		private static void ConvertToTileWayPrefab() => ConvertToPrefab(TileWayBaseBaseAssetLocator);

		private static void ConvertToPrefab(AssetLocator<GameObject> assetLocator)
		{
			assetLocator.SafeInvokeAsync(environmentAssetPrefab =>
				{
					var convertedObjects = ConvertToPrefab(GetFilteredAssets(), environmentAssetPrefab);

					Selection.objects = convertedObjects;
				})
				.Forget();
		}

		[MenuItem("Assets/" + Constants.MenuNames.MenuName + "/Create Environment Prefab", true)]
		[MenuItem("Assets/" + Constants.MenuNames.MenuName + "/Create Tile Prefab", true)]
		[MenuItem("Assets/" + Constants.MenuNames.MenuName + "/Create Tile Way Prefab", true)]
		private static bool ConvertToPrefabValidate()
		{
			if (Selection.count == 0)
			{
				return false;
			}

			return GetFilteredAssets().Length > 0;
		}

		private static GameObject[] GetFilteredAssets()
		{
			var filteredSelection =
				Selection.GetFiltered<GameObject>(SelectionMode.Assets)
					.Where(EditorUtility.IsPersistent)
					.Where(selection =>
					{
						var assetPath = AssetDatabase.GetAssetPath(selection);
						return assetPath.Contains(".fbx");
					})
					.ToArray();
			return filteredSelection;
		}

		private static Object[] ConvertToPrefab(GameObject[] gameObjects, GameObject basePrefab)
		{
			return gameObjects.Select(gameObject => ConvertToPrefab(gameObject, basePrefab))
				.Where(converted => converted is not null)
				.ToArray()!;
		}

		private static Object? ConvertToPrefab(GameObject gameObject, GameObject basePrefab)
		{
			var originalAssetPath = AssetDatabase.GetAssetPath(gameObject);
			var originalAssetFolder = Path.GetDirectoryName(originalAssetPath)!;
			var prefabAssetPath = Path.Combine(originalAssetFolder, $"{gameObject.name}.prefab");
			prefabAssetPath = AssetDatabase.GenerateUniqueAssetPath(prefabAssetPath);

			var basePrefabVariant = (GameObject)PrefabUtility.InstantiatePrefab(basePrefab);

			var gfxChild = basePrefabVariant.transform.Find("GFX");

			if (!gfxChild)
			{
				Debug.LogError(
					$"Can not create a prefab from {gameObject.name}. No child named \"GFX\" found in {basePrefab.name}", basePrefab);
				return null;
			}

			var staticFlags = GameObjectUtility.GetStaticEditorFlags(gfxChild.gameObject);

			var gameObjectInstance = (GameObject)PrefabUtility.InstantiatePrefab(gameObject, gfxChild);
			GameObjectUtility.SetStaticEditorFlags(gameObjectInstance, staticFlags);

			var savedPrefab = PrefabUtility.SaveAsPrefabAsset(basePrefabVariant, prefabAssetPath, out var success);
			GameObjectUtility.SetStaticEditorFlags(savedPrefab, staticFlags);

			if (basePrefabVariant)
			{
				Object.DestroyImmediate(basePrefabVariant);
			}

			if (!success)
			{
				Debug.Log($"Creating a prefab from {gameObject.name} was not successful", gameObject);
				return null;
			}

			return savedPrefab;
		}
	}
}
