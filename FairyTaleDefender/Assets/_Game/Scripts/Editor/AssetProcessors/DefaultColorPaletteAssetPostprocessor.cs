using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.AssetProcessors
{
	public class DefaultColorPaletteAssetPostprocessor : AssetPostprocessor
	{
		private const string ColorPaletteSummerGuid = "178b74dc53be54709b84d85209d03744";

		private Material? OnAssignMaterialModel(Material material, Renderer renderer)
		{
			if (material.name != "ColorPalette")
			{
				return null;
			}

			var importer = (ModelImporter)assetImporter;
			var existingRemaps = importer.GetExternalObjectMap();
			var hasColorPaletteRemap = existingRemaps.Any(kvp => kvp.Key.name == "ColorPalette");

			if (hasColorPaletteRemap)
			{
				return null;
			}

			return FindColorPaletteSummer();
		}

		private Material? FindColorPaletteSummer()
		{
			GUID.TryParse(ColorPaletteSummerGuid, out var guid);
			context.DependsOnSourceAsset(guid);
			var path = AssetDatabase.GUIDToAssetPath(ColorPaletteSummerGuid);
			return AssetDatabase.LoadAssetAtPath<Material>(path);
		}

		 public override uint GetVersion() => 2;
	}
}
