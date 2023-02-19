using BoundfoxStudios.CommunityProject.Entities.Characters.Enemies.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Systems.NavigationSystem;
using UnityEngine;
using UnityEngine.Splines;

namespace BoundfoxStudios.CommunityProject.Entities.Characters.Enemies
{
	[AddComponentMenu(Constants.MenuNames.Characters + "/" + nameof(Enemy))]
	public class Enemy : Character<EnemySO>
	{
		[field: SerializeField]
		public SplineWalker SplineWalker { get; set; } = default!;

		public void Initialize(ISpline spline)
		{
			SplineWalker.Initialize(spline, Definition.MovementSpeed);
		}

		private void OnEnable()
		{
			Health.Dead += Dead;
		}

		private void OnDisable()
		{
			Health.Dead -= Dead;
		}

		private void Dead()
		{
			Destroy(gameObject);
		}
	}
}
