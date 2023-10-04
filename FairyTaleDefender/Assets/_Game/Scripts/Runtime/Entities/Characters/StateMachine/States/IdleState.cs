using BoundfoxStudios.FairyTaleDefender.Entities.Characters.ScriptableObjects;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Characters.StateMachine.States
{
	public class IdleState<TDefinition> : BaseState<TDefinition>
		where TDefinition : CharacterSO
	{
		public override void Enter() { }

		public override void Exit() { }
	}
}
