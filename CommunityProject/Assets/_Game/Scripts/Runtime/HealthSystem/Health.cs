using System;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.HealthSystem
{
	[AddComponentMenu(Constants.MenuNames.HealthSystem + "/" + nameof(Health))]
	public class Health : MonoBehaviour
	{
		public event Action Dead = delegate { };
		public event Action Change = delegate { };
		public int Current { get; private set; }
		public int Maximum { get; private set; }

		public void Initialize(int health, int maxHealth)
		{
			Current = health;
			Maximum = maxHealth;

			Change();
		}

		public void TakeDamage(int damage)
		{
			Debug.Assert(damage > 0, "Damage is negative");

			Current -= damage;
			Change();

			if (Current <= 0)
			{
				Dead();
			}
		}
	}
}
