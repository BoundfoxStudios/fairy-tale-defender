using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting
{
	/// <summary>
	/// The target point is _the point_ any weapon will aim at and shoot to.
	/// </summary>
	[AddComponentMenu(Constants.MenuNames.Targeting + "/" + nameof(TargetPoint))]
	public class TargetPoint : MonoBehaviour
	{
		private Collider _targetPointCollider = default!;

		private void Awake()
		{
			_targetPointCollider = GetComponent<Collider>();

			if (!_targetPointCollider)
			{
				Debug.LogError("TargetPoint does not have a collider", this);
			}
		}

		public Vector3 Center => _targetPointCollider.bounds.center;
	}
}
