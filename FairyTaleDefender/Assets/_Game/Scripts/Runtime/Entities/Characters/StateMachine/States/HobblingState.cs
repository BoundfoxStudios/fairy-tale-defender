using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Characters.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.NavigationSystem;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Characters.StateMachine.States
{
	public class HobblingState<TDefinition> : BaseState<TDefinition>
		where TDefinition : CharacterSO
	{
		public override void Enter()
		{
			Character.Animator.SetBool(Constants.AnimationStates.IsHobbling, true);
			Character.GetComponent<SplineWalker>().MovementSpeed = Character.Definition.MovementSpeed * Character.Definition.HobbleSpeedPercentage;
		}

		public override void Exit() {}
	}
}
