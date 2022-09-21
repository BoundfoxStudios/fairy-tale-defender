using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BoundfoxStudios.CommunityProject.SceneManagement.ScriptableObjects
{
	/// <summary>
	/// Abstract scene definition.
	/// </summary>
	public abstract class SceneSO : ScriptableObject
	{
		public AssetReference SceneReference;
	}
}
