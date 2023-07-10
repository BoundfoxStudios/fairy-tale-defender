using BoundfoxStudios.FairyTaleDefender.Entities.Characters.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Entities.Characters.StateMachine;
using BoundfoxStudios.FairyTaleDefender.Systems.HealthSystem;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Characters
{
	public abstract class Character<TDefinition> : MonoBehaviour, IAmDamageable
		where TDefinition : CharacterSO
	{
		[field: SerializeField]
		public TDefinition Definition { get; set; } = default!;

		[field: SerializeField]
		public Health Health { get; private set; } = default!;

		[field: SerializeField]
		public Animator Animator { get; set; } = default!;

		protected CharacterStateMachine<TDefinition> StateMachine { get; private set; } = default!;

		protected virtual void Awake()
		{
			StateMachine = new(this);

			Health.Initialize(Definition.MaxHealth, Definition.MaxHealth);
		}

		protected void DestroyCharacter()
		{
			Destroy(gameObject);
		}
	}
}
