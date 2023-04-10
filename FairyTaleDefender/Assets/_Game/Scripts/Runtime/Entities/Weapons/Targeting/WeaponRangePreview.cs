using System;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Weapons.BallisticWeapons;
using BoundfoxStudios.FairyTaleDefender.Infrastructure;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting
{
	[RequireComponent(typeof(DecalProjector))]
	[AddComponentMenu(Constants.MenuNames.Targeting + "/" + nameof(WeaponRangePreview))]
	public class WeaponRangePreview : MonoBehaviour
	{
		[field: SerializeField]
		private float ProjectionDepth { get; set; } = 20f;

		private static readonly int MinMaxRange = Shader.PropertyToID("_MinMaxRange");
		private static readonly int AttackAngle = Shader.PropertyToID("_AttackAngle");

		private DecalProjector _decalProjector = default!;

		private void Awake()
		{
			_decalProjector = GetComponent<DecalProjector>();
		}

		public void DisplayWeaponRange(WeaponSelectedEventChannelSO.EventArgs eventArgs)
		{
			var rotation = Quaternion.Euler(90f, eventArgs.Transform.rotation.eulerAngles.y, 0f);
			transform.SetPositionAndRotation(eventArgs.Transform.position, rotation);

			var effectiveWeaponDefinition = eventArgs.EffectiveWeaponDefinition.CalculateEffectiveWeaponDefinition(eventArgs.Transform.position);

			var range = effectiveWeaponDefinition switch
			{
				EffectiveBallisticWeaponDefinition effectiveBallisticWeaponDefinition =>
					new Limits2(effectiveBallisticWeaponDefinition.MinimumRange,
						effectiveBallisticWeaponDefinition.MaximumRange),
				_ => throw new ArgumentOutOfRangeException(nameof(effectiveWeaponDefinition),
					$"{effectiveWeaponDefinition} is not implemented yet.")
			};

			SetProjectorSize(range.Maximum);
			SetShaderProperties(effectiveWeaponDefinition.AttackAngle, range);

			_decalProjector.enabled = true;
		}

		private void SetProjectorSize(float weaponRange) =>
			_decalProjector.size = new(weaponRange * 2, weaponRange * 2, ProjectionDepth);

		private void SetShaderProperties(float attackAngle, Limits2 weaponRange)
		{
			_decalProjector.material.SetFloat(AttackAngle, attackAngle);
			_decalProjector.material.SetVector(MinMaxRange, weaponRange);
		}

		public void StopDisplayingWeaponRange()
		{
			_decalProjector.enabled = false;
		}
	}
}
