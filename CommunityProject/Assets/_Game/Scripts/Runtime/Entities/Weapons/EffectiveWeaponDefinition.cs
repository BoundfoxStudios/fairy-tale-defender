namespace BoundfoxStudios.CommunityProject.Entities.Weapons
{
	public abstract class EffectiveWeaponDefinition
	{
		public float Range { get; protected set; }
		public float FireRateInSeconds { get; protected set; }
		public int AttackAngle { get; protected set; }
	}
}
