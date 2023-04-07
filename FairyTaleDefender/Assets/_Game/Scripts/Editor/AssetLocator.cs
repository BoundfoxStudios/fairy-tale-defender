using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BoundfoxStudios.FairyTaleDefender.Editor
{
	public class AssetLocator<T>
		where T : class
	{
		private readonly string _addressablesKey;
		private T? _locatedAsset;

		public AssetLocator(string addressablesKey)
		{
			_addressablesKey = addressablesKey;
		}

		private async UniTask<T?> LocateAssetAsync()
		{
			var asset = await Addressables.LoadAssetAsync<T>(_addressablesKey);

			if (asset is null)
			{
				var assetTypeName = typeof(T).Name;
				Debug.LogWarning($"Did not find {assetTypeName} under addressables key {_addressablesKey}");
				return null;
			}

			return asset;
		}

		public async UniTask SafeInvokeAsync(Action<T> callback)
		{
			_locatedAsset ??= await LocateAssetAsync();

			if (_locatedAsset is null)
			{
				return;
			}

			callback(_locatedAsset);
		}
	}
}
