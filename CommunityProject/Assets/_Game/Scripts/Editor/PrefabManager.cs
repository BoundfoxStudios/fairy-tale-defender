using System;
using BoundfoxStudios.CommunityProject.EditorExtensions.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BoundfoxStudios.CommunityProject.Editor
{
	public static class PrefabManager
	{
		private const string PrefabManagerAddressablesKey = "ScriptableObjects/PrefabManager.asset";
		private static PrefabManagerSO? _prefabManager;

		private static async UniTask<PrefabManagerSO?> LocatePrefabManagerAsync()
		{
			var prefabManager = await Addressables.LoadAssetAsync<PrefabManagerSO>(PrefabManagerAddressablesKey);

			if (!prefabManager)
			{
				Debug.LogWarning($"Did not find {nameof(prefabManager)} under addressables key {PrefabManagerAddressablesKey}");
				return null;
			}

			return prefabManager;
		}

		public static async UniTask SafeInvokeAsync(Action<PrefabManagerSO> callback)
		{
			_prefabManager ??= await LocatePrefabManagerAsync();

			if (_prefabManager is null)
			{
				return;
			}

			callback(_prefabManager);
		}
	}
}
