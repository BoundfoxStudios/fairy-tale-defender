using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.SaveGameSystem.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.RuntimeAnchors + "/Save Game")]
	public class SaveGameRuntimeAnchorSO : RuntimeAnchorBaseSO<SaveGame>
	{
		// Marker class.
	}
}
