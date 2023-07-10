using BoundfoxStudios.FairyTaleDefender.Entities.Characters.ScriptableObjects;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Characters.StateMachine
{
	public interface IState<TDefinition>
		where TDefinition : CharacterSO
	{
		void Initialize(Character<TDefinition> character, CharacterStateMachine<TDefinition> stateMachine);
		void Enter();
		void Exit();
	}
}
