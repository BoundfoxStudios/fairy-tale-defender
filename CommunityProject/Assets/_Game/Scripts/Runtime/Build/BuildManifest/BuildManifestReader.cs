using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BoundfoxStudios.CommunityProject.Build.BuildManifest
{
	/// <summary>
	/// This class can read the build manifest.json containing additional information about the build environment.
	/// </summary>
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
