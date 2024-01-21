using System;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting
{
	public interface ICanTrackTarget
	{
		event Action TargetChanged;
		TargetPoint? Target { get; }
	}
}
