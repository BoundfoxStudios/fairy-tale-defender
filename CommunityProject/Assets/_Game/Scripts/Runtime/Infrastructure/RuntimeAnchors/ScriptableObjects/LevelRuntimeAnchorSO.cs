using BoundfoxStudios.CommunityProject.Infrastructure.SceneManagement.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Infrastructure.RuntimeAnchors.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.RuntimeAnchors + "/Level")]
	public class LevelRuntimeAnchorSO : RuntimeAnchorBaseSO<LevelSO>
	{
		// Marker class.
	}
}
