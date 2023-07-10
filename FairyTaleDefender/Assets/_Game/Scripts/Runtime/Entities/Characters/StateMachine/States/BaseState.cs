using BoundfoxStudios.FairyTaleDefender.Entities.Characters.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Characters.StateMachine.States
{
	public abstract class BaseState<TDefinition> : IState<TDefinition>
		where TDefinition : CharacterSO
	{
		protected Character<TDefinition> Character { get; private set; } = default!;
		protected CharacterStateMachine<TDefinition> CharacterStateMachine { get; private set; } = default!;

		public void Initialize(Character<TDefinition> character, CharacterStateMachine<TDefinition> stateMachine)
		{
			Character = character;
			CharacterStateMachine = stateMachine;
		}

		public abstract void Enter();
		public abstract void Exit();
	}
}
