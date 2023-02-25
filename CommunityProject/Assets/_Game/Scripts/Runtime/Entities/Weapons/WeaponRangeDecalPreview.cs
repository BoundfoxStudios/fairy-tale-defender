using System;
using BoundfoxStudios.CommunityProject.Entities.Weapons.BallisticWeapons;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace BoundfoxStudios.CommunityProject.Entities.Weapons
{
	[RequireComponent(typeof(DecalProjector))]
	[AddComponentMenu(Constants.MenuNames.Targeting + "/" + nameof(WeaponRangeDecalPreview))]
	public class WeaponRangeDecalPreview : MonoBehaviour
	{
		[field: SerializeField]
		private float ProjectionDepth { get; set; } = 20f;

		private static readonly int MinMaxRange = Shader.PropertyToID("_MinMaxRange");
		private static readonly int AttackAngle = Shader.PropertyToID("_AttackAngle");

		private DecalProjector _decalProjector = default!;

		private const float ProjectorSizeFactor = 0.01f;

		private void Awake()
		{
			_decalProjector = GetComponent<DecalProjector>();
		}

		private void DisplayWeaponRange(Vector3 weaponPosition, EffectiveWeaponDefinition weaponDefinition)
		{
			transform.position = weaponPosition;

			Vector3 range = weaponDefinition switch
			{
				EffectiveBallisticWeaponDefinition effectiveBallisticWeaponDefinition =>
					new Vector3(effectiveBallisticWeaponDefinition.MinimumRange,
						effectiveBallisticWeaponDefinition.MaximumRange),
				_ => throw new ArgumentOutOfRangeException(nameof(weaponDefinition),
					$"{weaponDefinition} is not implemented yet.")
			};

			SetProjectorSize(range.y);
			SetShaderProperties(weaponDefinition.AttackAngle, range);

			_decalProjector.enabled = true;
		}

		private void SetProjectorSize(float weaponRange)
		{
			var boxWidth = weaponRange * 2 - ProjectorSizeFactor;
			var newSize = new Vector3(boxWidth, boxWidth, ProjectionDepth);
			_decalProjector.size = newSize;
		}

		private void SetShaderProperties(float attackAngle, Vector3 weaponRange)
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
