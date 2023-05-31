using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.AssetProcessors
{
	public class DefaultMaterialsAssetPostprocessor : AssetPostprocessor
	{
		private const string ColorPaletteSummerMaterialGuid = "178b74dc53be54709b84d85209d03744";
		private const string TileSurfaceMaterialGuid = "4a52ce37bf16a42a29e689c796f909ac";

		private Material? OnAssignMaterialModel(Material material, Renderer renderer)
		{
			if (TryHandleMaterial(material, "ColorPalette", FindColorPaletteSummerMaterial, out var colorPaletteMaterial))
			{
				return colorPaletteMaterial;
			}

			if (TryHandleMaterial(material, "Surface", FindTileSurfaceMaterial, out var surfaceMaterial))
			{
				return surfaceMaterial;
			}

			return null;
		}

		private bool TryHandleMaterial(Material material, string name, Func<Material?> materialAccessor,
			out Material? materialToApply)
		{
			materialToApply = null;

			if (material.name != name)
			{
				return false;
			}

			var importer = (ModelImporter)assetImporter;
			var existingRemaps = importer.GetExternalObjectMap();
			var hasRemap = existingRemaps.Any(kvp => kvp.Key.name == name);

			if (hasRemap)
			{
				return false;
			}

			materialToApply = materialAccessor();

			return true;
		}

		private Material? FindColorPaletteSummerMaterial() => FindMaterial(ColorPaletteSummerMaterialGuid);

		private Material? FindTileSurfaceMaterial() => FindMaterial(TileSurfaceMaterialGuid);

		private Material? FindMaterial(string guidString)
		{
			GUID.TryParse(guidString, out var guid);
			context.DependsOnSourceAsset(guid);
			var path = AssetDatabase.GUIDToAssetPath(guidString);
			return AssetDatabase.LoadAssetAtPath<Material>(path);
		}

		public override uint GetVersion() => 5;
	}
}
