using UnityEngine;
using UnityEngine.Localization;

namespace BoundfoxStudios.CommunityProject.Infrastructure.SceneManagement.ScriptableObjects
{
	/// <summary>
	/// Describes a playable level.
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.SceneManagement + "/Level")]
	public class LevelSO : SceneSO
	{
		[field: SerializeField]
		public LocalizedString Name { get; private set; } = default!;
	}
}
