using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Entities.Buildings.Towers
{
	[SelectionBase]
	public class Tower : MonoBehaviour
	{
		[field: SerializeField]
		public Collider Collider { get; private set; } = default!;
	}
}
