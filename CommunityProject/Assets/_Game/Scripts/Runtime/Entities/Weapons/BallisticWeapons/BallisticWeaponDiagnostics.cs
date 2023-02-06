using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Entities.Weapons.BallisticWeapons
{
	/// <summary>
	/// Helper class for Gizmos, see Editor Assembly -> BallisticWeaponGizmos
	/// </summary>
	[AddComponentMenu(Constants.MenuNames.Weapons + "/" + nameof(BallisticWeaponDiagnostics))]
	public class BallisticWeaponDiagnostics : MonoBehaviour
	{
		[field: SerializeField]
		public BallisticWeapon Weapon { get; private set; } = default!;
	}
}
