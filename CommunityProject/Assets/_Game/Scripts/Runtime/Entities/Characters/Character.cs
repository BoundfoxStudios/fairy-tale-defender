using BoundfoxStudios.CommunityProject.Entities.Characters.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Systems.HealthSystem;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Entities.Characters
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

		protected void DestroyCharacter()
		{
			Destroy(gameObject);
		}
	}
}
