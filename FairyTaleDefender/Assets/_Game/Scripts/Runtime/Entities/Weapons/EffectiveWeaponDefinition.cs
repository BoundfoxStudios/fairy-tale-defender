namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons
{
	public abstract class EffectiveWeaponDefinition
	{
		public float Range { get; protected set; }
		public float FireRateEverySeconds { get; protected set; }
		public int AttackAngle { get; protected set; }
	}
}
