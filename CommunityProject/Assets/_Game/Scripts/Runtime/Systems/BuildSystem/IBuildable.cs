using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Systems.BuildSystem
{
	public interface IBuildable
	{
		/// <summary>
		/// The real object to place.
		/// </summary>
		GameObject Prefab { get; }

		/// <summary>
		/// A graphics only object used a ghost to show to the player.
		/// </summary>
		GameObject BlueprintPrefab { get; }
	}
}
