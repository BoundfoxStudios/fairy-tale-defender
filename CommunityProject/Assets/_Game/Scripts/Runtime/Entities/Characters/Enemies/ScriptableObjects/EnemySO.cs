using BoundfoxStudios.CommunityProject.Entities.Characters.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Entities.Characters.Enemies.ScriptableObjects
{
	/// <summary>
	/// Base Information about an enemy
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.Characters + "/Enemy")]
	public class EnemySO : CharacterSO
	{
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
	}
}
