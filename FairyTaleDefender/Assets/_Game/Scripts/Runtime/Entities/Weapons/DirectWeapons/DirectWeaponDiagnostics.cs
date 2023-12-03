using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.DirectWeapons
{
	/// <summary>
	/// Helper class for Gizmos, see Editor Assembly -> BallisticWeaponGizmos
	/// </summary>
	[AddComponentMenu(Constants.MenuNames.Weapons + "/" + nameof(DirectWeaponDiagnostics))]
	public class DirectWeaponDiagnostics : MonoBehaviour
	{
		[field: SerializeField]
		public DirectWeapon Weapon { get; private set; } = default!;
	}
}
