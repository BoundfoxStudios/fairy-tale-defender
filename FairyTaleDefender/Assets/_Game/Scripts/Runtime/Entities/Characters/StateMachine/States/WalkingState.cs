using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Characters.ScriptableObjects;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Characters.StateMachine.States
{
	public class WalkingState<TDefinition> : BaseState<TDefinition>
		where TDefinition : CharacterSO
	{
		public override void Enter()
		{
			Character.Animator.SetBool(Constants.AnimationStates.IsWalking, true);

			Character.Health.Change += HealthChange;
		}

		public override void Exit()
		{
			Character.Health.Change -= HealthChange;

			Character.Animator.SetBool(Constants.AnimationStates.IsWalking, false);
		}

		private void HealthChange(int current, int change)
		{
			if (Character.Definition.CanHobble && (float)current / Character.Health.Maximum < 0.35f)
			{
				CharacterStateMachine.ChangeState<HobblingState<TDefinition>>();
			}
		}
	}
}
