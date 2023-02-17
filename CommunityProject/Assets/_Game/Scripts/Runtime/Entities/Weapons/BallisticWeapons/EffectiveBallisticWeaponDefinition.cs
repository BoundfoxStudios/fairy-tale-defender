using BoundfoxStudios.CommunityProject.Infrastructure;

namespace BoundfoxStudios.CommunityProject.Entities.Weapons.BallisticWeapons
{
	public class EffectiveBallisticWeaponDefinition : EffectiveWeaponDefinition
	{
		public float MinimumRange
		{
			get => _minimumRange;
			private set
			{
				_minimumRange = value;
				_range.Minimum = value;
			}
		}

		public float MaximumRange
		{
			get => base.Range;
			private set
			{
				base.Range = value;
				_range.Maximum = value;
			}
		}

		public new Limits2 Range
		{
			get => _range;
			private set
			{
				_range = value;
				MinimumRange = value.Minimum;
				MaximumRange = value.Maximum;
			}
		}

		private float _minimumRange;
		private Limits2 _range;

		public EffectiveBallisticWeaponDefinition(Limits2 range, float fireRateInSeconds, int attackAngle)
		{
			Range = range;
			FireRateInSeconds = fireRateInSeconds;
			AttackAngle = attackAngle;
		}
	}
}
