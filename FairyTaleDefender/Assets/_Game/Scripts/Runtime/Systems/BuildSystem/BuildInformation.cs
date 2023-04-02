using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.BuildSystem
{
	[AddComponentMenu(Constants.MenuNames.BuildSystem + "/" + nameof(BuildInformation))]
	public class BuildInformation : MonoBehaviour
	{
		[field: SerializeField]
		public bool IsBuildable { get; private set; }
	}
}
