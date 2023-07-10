using UnityEngine;
using UnityEngine.Localization;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Characters.ScriptableObjects
{
	public abstract class CharacterSO : ScriptableObject
	{
		[field: SerializeField]
		public LocalizedString Name { get; private set; } = default!;

		[field: SerializeField]
		[field: Min(1)]
		public int MaxHealth { get; private set; } = 1;

		[field: SerializeField]
		[field: Min(0)]
		public int Armor { get; private set; } = 1;

		[field: SerializeField]
		[field: Min(0)]
		public float MovementSpeed { get; private set; } = 1f;

		[field: SerializeField]
		public bool CanHobble { get; private set; }

		[field: SerializeField]
		[field: Tooltip("MovementSpeed will be multiplied with this value when a character starts to hobble")]
		[field: Range(0, 1f)]
		public float HobbleSpeedPercentage { get; private set; } = 0.33f;
	}
}
