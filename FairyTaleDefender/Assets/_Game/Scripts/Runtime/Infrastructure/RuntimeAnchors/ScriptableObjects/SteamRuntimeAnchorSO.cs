using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Common.Integrations.Steam;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.RuntimeAnchors + "/Steam")]
	public class SteamRuntimeAnchorSO : RuntimeAnchorBaseSO<SteamManager>
	{
		// Marker class.
	}
}
