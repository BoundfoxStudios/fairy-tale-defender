using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting.ScriptableObjects;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting
{
	public interface ICanChangeTargetType
	{
		TargetTypeSO TargetType { get; set; }
	}
}
