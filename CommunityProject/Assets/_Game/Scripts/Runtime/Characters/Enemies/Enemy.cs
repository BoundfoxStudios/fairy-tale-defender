using BoundfoxStudios.CommunityProject.Characters.Enemies.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Characters.Enemies
{
	[AddComponentMenu(Constants.MenuNames.Characters + "/" + nameof(Enemy))]
	public class Enemy : Character<EnemySO>
	{
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
