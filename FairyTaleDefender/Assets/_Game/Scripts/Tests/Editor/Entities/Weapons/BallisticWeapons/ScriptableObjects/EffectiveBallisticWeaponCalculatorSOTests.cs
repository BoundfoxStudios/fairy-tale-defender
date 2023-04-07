using BoundfoxStudios.FairyTaleDefender.Editor.Extensions;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.ScriptableObjects;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Tests.Editor.Entities.Weapons.BallisticWeapons.ScriptableObjects
{
	public class EffectiveBallisticWeaponCalculatorSOTests
	{
		[TestCase(0, 4)] // No y-Position change -> no change in expected range
		[TestCase(-10, 4)] // Everything below y < 0 -> no change in expected range
		[TestCase(1, 6)]
		[TestCase(2, 8)]
		[TestCase(10, 24)]
		public void DoesChangeAddRangeDependingOnTheTowersPosition(float towerYPosition, float expectedMaximumRange,
			float definedMaximumRange = 4, float heightToRangeFactor = 2)
		{
			var sut = CreateSystemUnderTest(heightToRangeFactor);
			var ballisticWeapon = CreateBallisticWeapon(definedMaximumRange);

			var result = sut.Calculate(ballisticWeapon, new(0, towerYPosition, 0));

			result.MaximumRange.Should().BeApproximately(expectedMaximumRange, 0.001f);
		}

		private BallisticWeaponSO CreateBallisticWeapon(float maximumRange)
		{
			var ballisticWeapon = ScriptableObject.CreateInstance<BallisticWeaponSO>();

			var serializedObject = new SerializedObject(ballisticWeapon);
			SetPrivateProperty(serializedObject, nameof(BallisticWeaponSO.MinimumRange), 1f);

			// We need to set the base range here, because BallisticWeaponSO redefines "Range" to be a Limit2.
			SetPrivateProperty(serializedObject, nameof(WeaponSO.Range), maximumRange);
			return ballisticWeapon;
		}

		private EffectiveBallisticWeaponCalculatorSO CreateSystemUnderTest(float heightToRangeFactor = 2)
		{
			var sut = ScriptableObject.CreateInstance<EffectiveBallisticWeaponCalculatorSO>();

			var serializedObject = new SerializedObject(sut);
			SetPrivateProperty(serializedObject, nameof(EffectiveBallisticWeaponCalculatorSO.HeightToRangeFactor),
				heightToRangeFactor);
			return sut;
		}

		private void SetPrivateProperty<T>(SerializedObject serializedObject, string propertyName, T value)
		{
			var property = serializedObject.FindRealProperty(propertyName);
			property.SetValue(value);
			serializedObject.ApplyModifiedPropertiesWithoutUndo();
		}
	}
}
