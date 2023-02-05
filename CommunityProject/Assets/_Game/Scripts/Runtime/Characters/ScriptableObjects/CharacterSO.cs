using UnityEngine;
using UnityEngine.Localization;

namespace BoundfoxStudios.CommunityProject.Characters.ScriptableObjects
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
	}
}
