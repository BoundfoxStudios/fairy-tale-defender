using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.AssetProcessors
{
	public class DefaultColorPalette : AssetPostprocessor
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

			importer.AddRemap(new(typeof(Material), "ColorPalette"), FindColorPaletteSummer());

			return null;
		}

		private Material? FindColorPaletteSummer()
		{
			var path = AssetDatabase.GUIDToAssetPath(ColorPaletteSummerGuid);
			return AssetDatabase.LoadAssetAtPath<Material>(path);
		}
	}
}
