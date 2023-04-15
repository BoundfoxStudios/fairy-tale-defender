using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.AssetProcessors
{
	public class DefaultColorPalette : AssetPostprocessor
	{
		private const string ColorPaletteSummerGuid = "178b74dc53be54709b84d85209d03744";

		private Material? OnAssignMaterialModel(Material material, Renderer renderer)
		{
			if (!renderer.sharedMaterial && material.name == "ColorPalette")
			{
				return FindColorPaletteSummer();
			}

			return null;
		}

		private Material? FindColorPaletteSummer()
		{
			var path = AssetDatabase.GUIDToAssetPath(ColorPaletteSummerGuid);
			return AssetDatabase.LoadAssetAtPath<Material>(path);
		}
	}
}
