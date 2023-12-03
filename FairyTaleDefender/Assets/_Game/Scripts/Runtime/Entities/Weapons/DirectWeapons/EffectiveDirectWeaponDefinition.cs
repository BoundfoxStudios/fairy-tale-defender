namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.DirectWeapons
{
	public class EffectiveDirectWeaponDefinition : EffectiveWeaponDefinition
	{
		public EffectiveDirectWeaponDefinition(float range, int attackAngle, float fireRateEverySeconds)
		{
			Range = range;
			AttackAngle = attackAngle;
			FireRateEverySeconds = fireRateEverySeconds;
		}
	}
}
