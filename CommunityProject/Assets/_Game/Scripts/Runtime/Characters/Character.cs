using BoundfoxStudios.CommunityProject.Characters.ScriptableObjects;
using BoundfoxStudios.CommunityProject.HealthSystem;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Characters
{
	public abstract class Character<TDefinition> : MonoBehaviour, IAmDamageable
		where TDefinition : CharacterSO
	{
		[field: SerializeField]
		protected TDefinition Definition { get; set; } = default!;

		[field: SerializeField]
		public Health Health { get; private set; } = default!;

		private void Awake()
		{
			Health.Initialize(Definition.MaxHealth, Definition.MaxHealth);
		}
	}
}
