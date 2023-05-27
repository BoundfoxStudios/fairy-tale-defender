using BoundfoxStudios.FairyTaleDefender.Entities.Characters.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.HealthSystem;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Characters
{
	public abstract class Character<TDefinition> : MonoBehaviour, IAmDamageable
		where TDefinition : CharacterSO
	{
		[field: SerializeField]
		protected TDefinition Definition { get; set; } = default!;

		[field: SerializeField]
		public Health Health { get; private set; } = default!;

		[field: SerializeField]
		protected Animator Animator { get; set; } = default!;

		protected virtual void Awake()
		{
			Health.Initialize(Definition.MaxHealth, Definition.MaxHealth);
		}

		protected void DestroyCharacter()
		{
			Destroy(gameObject);
		}
	}
}
