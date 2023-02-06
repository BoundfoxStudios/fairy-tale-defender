using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BoundfoxStudios.CommunityProject.Infrastructure.SceneManagement.ScriptableObjects
{
	/// <summary>
	/// Abstract scene definition.
	/// </summary>
	public abstract class SceneSO : ScriptableObject
	{
		[field: SerializeField]
		public AssetReference SceneReference { get; private set; } = default!;
	}
}
