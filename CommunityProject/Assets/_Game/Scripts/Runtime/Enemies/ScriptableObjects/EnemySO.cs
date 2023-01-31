using BoundfoxStudios.CommunityProject.Infrastructure;
using UnityEngine;
using UnityEngine.Localization;

namespace BoundfoxStudios.CommunityProject.Enemies.ScriptableObjects
{
	/// <summary>
	/// Base Information about an enemy
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.Enemies + "/Enemy")]
	public class EnemySO : ScriptableObject
	{
		[field: SerializeField]
		public LocalizedString Name { get; private set; } = default!;

		[field: SerializeField]
		[field: Min(1)]
		public int MaxHealth { get; private set; } = 1;

		[field: SerializeField]
		[field: Min(0)]
		public int Armor { get; private set; } = 1;

		[field: SerializeField]
		[field: Min(0)]
		public float MovementSpeed { get; private set; } = 1f;

		[field: SerializeField]
		[field: Min(0)]
		[field: Tooltip("Size of this enemy in meter.")]
		public float Size { get; private set; } = 1f;

		[field: SerializeField]
		[field: Min(0)]
		[field: Tooltip("The amount of experience awarded to player on kill.")]
		public int ExperienceOnKill { get; private set; } = 1;

		[field: SerializeField]
		[field: Min(0)]
		[field: Tooltip("The amount of currency awarded to player on kill.")]
		public int CurrencyOnKill { get; private set; } = 1;

		private void Awake()
		{
			Guard.AgainstNull(() => Name, this);
		}
	}
}
