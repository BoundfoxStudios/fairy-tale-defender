using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.BuildSystem
{
	public interface IAmBuildable
	{
		/// <summary>
		/// The real object to place.
		/// </summary>
		GameObject Prefab { get; }

		/// <summary>
		/// A graphics only object used a ghost to show to the player.
		/// </summary>
		GameObject BlueprintPrefab { get; }

		/// <summary>
		/// An image to display in UI e.g. in build buttons.
		/// </summary>
		Sprite BuildableIcon { get; }
	}
}
