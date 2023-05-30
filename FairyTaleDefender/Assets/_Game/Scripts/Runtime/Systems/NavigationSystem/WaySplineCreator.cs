using UnityEngine;
using UnityEngine.Splines;

namespace BoundfoxStudios.FairyTaleDefender.Systems.NavigationSystem
{
	public class WaySplineCreator : MonoBehaviour
	{
		[field: SerializeField]
		public SplineContainer SplineContainer { get; private set; } = default!;

		[field: SerializeField]
		public GameObject WayContainer { get; private set; } = default!;

		[field: SerializeField]
		public GameObject StartTile { get; private set; } = default!;

		[field: SerializeField]
		public GameObject EndTile { get; private set; } = default!;
	}
}
