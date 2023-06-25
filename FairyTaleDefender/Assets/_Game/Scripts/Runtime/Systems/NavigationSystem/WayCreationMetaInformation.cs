using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;
using UnityEngine.Splines;

namespace BoundfoxStudios.FairyTaleDefender.Systems.NavigationSystem
{
	/// <summary>
	/// Helper class for Gizmos, see Editor Assembly -> SplineContainerGizmos
	/// </summary>
	[AddComponentMenu(Constants.MenuNames.Navigation + "/" + nameof(WayCreationMetaInformation))]
	public class WayCreationMetaInformation : MonoBehaviour
	{
		[field: SerializeField]
		public SplineContainer? SplineContainer { get; private set; }

		[field: SerializeField]
		public bool IsSpawnPoint { get; private set; }

		[field: SerializeField]
		public bool HasExit { get; private set; }

		public int ExitIndex { get; set; }

		[field: SerializeField]
		public SplineKnotIndex[] Exits { get; private set; } = default!;

	}
}
