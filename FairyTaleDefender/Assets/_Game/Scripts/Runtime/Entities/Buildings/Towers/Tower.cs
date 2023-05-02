using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Buildings.Towers
{
	[SelectionBase]
	[AddComponentMenu(Constants.MenuNames.Towers + "/" + nameof(Tower))]
	public class Tower : MonoBehaviour
	{
		[field: SerializeField]
		public Collider Collider { get; private set; } = default!;
	}
}
