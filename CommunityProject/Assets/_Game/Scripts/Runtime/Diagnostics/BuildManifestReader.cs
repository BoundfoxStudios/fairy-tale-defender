using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BoundfoxStudios.CommunityProject.Diagnostics
{
	public class BuildManifestReader
	{
		private const string BuildManifestAddressablesKey = "Build/manifest.json";

		public async UniTask<BuildManifest> LoadAsync()
		{
			var textAsset = await Addressables.LoadAssetAsync<TextAsset>(BuildManifestAddressablesKey);
			var buildManifest = JsonUtility.FromJson<BuildManifest>(textAsset.text);

			return buildManifest;
		}
	}
}
