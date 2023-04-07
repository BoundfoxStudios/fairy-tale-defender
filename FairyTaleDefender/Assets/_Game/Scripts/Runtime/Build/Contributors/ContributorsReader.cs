using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BoundfoxStudios.CommunityProject.Build.Contributors
{
	/// <summary>
	/// This class can read the contributors.json.
	/// </summary>
	public class ContributorsReader
	{
		private const string ContributorsAddressablesKey = "Build/contributors.json";

		public async UniTask<Contributor[]> LoadAsync()
		{
			var textAsset = await Addressables.LoadAssetAsync<TextAsset>(ContributorsAddressablesKey);
			var contributors = JsonUtility.FromJson<Contributors>(textAsset.text);

			return contributors.Items;
		}
	}
}
