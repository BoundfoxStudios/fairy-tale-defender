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

		public void Initialize(ISpline spline)
		{
			SplineWalker.Initialize(spline, Definition.MovementSpeed);
		}

		private void OnEnable()
		{
			Health.Dead += Dead;
			SplineWalker.ReachedEndOfSpline += ReachedEndOfSpline;
		}

		private void OnDisable()
		{
			Health.Dead -= Dead;
			SplineWalker.ReachedEndOfSpline -= ReachedEndOfSpline;
		}

		private void ReachedEndOfSpline()
		{
			// TODO: Get this info from the SO
			EnemyDamagesPlayerEventChannel.Raise(1);
			Dead();
		}

		private void Dead()
		{
			Destroy(gameObject);
		}
	}
}
