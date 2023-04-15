using BoundfoxStudios.FairyTaleDefender.Infrastructure;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting.ScriptableObjects
{
	public abstract class TargetTypeSO : ScriptableObject
	{
		public abstract Collider GetTargetNonAlloc(Vector3 weaponPosition, NoAllocArrayResult<Collider> targets);
	}
}
