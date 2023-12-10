using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Characters.Enemies.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Entities.Characters.StateMachine.States;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.NavigationSystem;
using UnityEngine;
using UnityEngine.Splines;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Characters.Enemies
{
	[SelectionBase]
	[AddComponentMenu(Constants.MenuNames.Characters + "/" + nameof(Enemy))]
	public class Enemy : Character<EnemySO>
	{
		[field: Header("References")]
		[field: SerializeField]
		private SplineWalker SplineWalker { get; set; } = default!;

		[field: Header("Broadcasting Channels")]
		[field: SerializeField]
		private IntEventChannelSO EnemyDamagesPlayerEventChannel { get; set; } = default!;

		[field: SerializeField]
		public EnemySOEventChannelSO EnemyDestroyedByPlayer { get; private set; } = default!;

		[field: SerializeField]
		public EnemyEventChannelSO EnemyDestroyed { get; private set; } = default!;

		protected override void Awake()
		{
			base.Awake();

			StateMachine.AddState<WalkingState<EnemySO>>();
			StateMachine.AddState<HobblingState<EnemySO>>();

			StateMachine.ChangeState<WalkingState<EnemySO>>();
		}

		public void Initialize(ISpline spline)
		{
			SplineWalker.Initialize(spline, Definition.MovementSpeed);
		}

		private void OnEnable()
		{
			Health.Dead += DestroyedByPlayer;
			SplineWalker.ReachedEndOfSpline += ReachedEndOfSpline;
		}

		private void OnDisable()
		{
			Health.Dead -= DestroyedByPlayer;
			SplineWalker.ReachedEndOfSpline -= ReachedEndOfSpline;
		}

		private void ReachedEndOfSpline()
		{
			// TODO: Get this info from the SO
			EnemyDamagesPlayerEventChannel.Raise(1);
			Destroy();
		}

		private void DestroyedByPlayer()
		{
			EnemyDestroyedByPlayer.Raise(Definition);
			Destroy();
		}

		private void Destroy()
		{
			EnemyDestroyed.Raise(this);
			DestroyCharacter();
		}
	}
}
