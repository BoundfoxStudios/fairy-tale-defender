using System;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.HealthSystem
{
	[AddComponentMenu(Constants.MenuNames.HealthSystem + "/" + nameof(Health))]
	public class Health : MonoBehaviour
	{
		public delegate void HealthHandler(int current, int change);

		[field: SerializeField]
		private HealthVisualizerRuntimeAnchorSO? HealthVisualizerRuntimeAnchor { get; set; }

		public event Action Dead = delegate { };
		public event HealthHandler Change = delegate { };
		public int Current { get; private set; }
		public int Maximum { get; private set; }

		public void Initialize(int health, int maxHealth)
		{
			Current = health;
			Maximum = maxHealth;

			OnChange(health, 0);
		}

		public void TakeDamage(int damage)
		{
			Debug.Assert(damage >= 0, "Damage is negative");

			Current -= damage;
			OnChange(Current, damage);

			if (Current <= 0)
			{
				Dead();
			}
		}

		private void OnChange(int current, int change)
		{
			Change(current, change);

			if (HealthVisualizerRuntimeAnchor && HealthVisualizerRuntimeAnchor!.IsSet)
			{
				HealthVisualizerRuntimeAnchor.ItemSafe.UpdateVisuals(current, Maximum);
			}
		}
	}
}
