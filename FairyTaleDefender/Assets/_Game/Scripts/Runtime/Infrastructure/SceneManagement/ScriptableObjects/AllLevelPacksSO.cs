using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Infrastructure.SceneManagement.ScriptableObjects
{
	/// <summary>
	/// Collection of all level packs
	/// </summary>
	// We don't need more than one instance.
	// [CreateAssetMenu(menuName = Constants.MenuNames.SceneManagement + "/All Level Packs")]
	public class AllLevelPacksSO : ScriptableObject
	{
		[field: SerializeField]
		public LevelPackSO[] LevelPacks { get; private set; } = default!;
	}
}
