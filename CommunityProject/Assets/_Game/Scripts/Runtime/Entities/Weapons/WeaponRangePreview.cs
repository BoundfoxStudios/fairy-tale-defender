using System;
using BoundfoxStudios.CommunityProject.Entities.Weapons.BallisticWeapons;
using BoundfoxStudios.CommunityProject.Infrastructure;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace BoundfoxStudios.CommunityProject.Entities.Weapons
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

		private void DisplayWeaponRange(Vector3 weaponPosition, EffectiveWeaponDefinition weaponDefinition)
		{
			transform.position = weaponPosition;

			var range = weaponDefinition switch
			{
				EffectiveBallisticWeaponDefinition effectiveBallisticWeaponDefinition =>
					new Limits2(effectiveBallisticWeaponDefinition.MinimumRange,
						effectiveBallisticWeaponDefinition.MaximumRange),
				_ => throw new ArgumentOutOfRangeException(nameof(weaponDefinition),
					$"{weaponDefinition} is not implemented yet.")
			};

			SetProjectorSize(range.Maximum);
			SetShaderProperties(weaponDefinition.AttackAngle, range);

			_decalProjector.enabled = true;
		}

		private void SetProjectorSize(float weaponRange) =>
			_decalProjector.size = new(weaponRange * 2, weaponRange * 2, ProjectionDepth);

		private void SetShaderProperties(float attackAngle, Limits2 weaponRange)
		{
			_decalProjector.material.SetFloat(AttackAngle, attackAngle);
			_decalProjector.material.SetVector(MinMaxRange, weaponRange);
		}

		private void StopDisplayingWeaponRange()
		{
			_decalProjector.enabled = false;
		}
	}
}
