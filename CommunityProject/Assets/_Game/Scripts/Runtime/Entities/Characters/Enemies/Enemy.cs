using BoundfoxStudios.CommunityProject.Entities.Characters.Enemies.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Systems.NavigationSystem;
using UnityEngine;
using UnityEngine.Splines;

namespace BoundfoxStudios.CommunityProject.Entities.Characters.Enemies
{
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
		public EnemyEventChannelSO EnemyDestroyedByPlayer { get; private set; } = default!;

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
			DestroyCharacter();
		}

		private void DestroyedByPlayer()
		{
			EnemyDestroyedByPlayer.Raise(Definition);
			DestroyCharacter();
		}
	}
}
