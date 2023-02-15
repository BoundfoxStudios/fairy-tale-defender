using System;
using BoundfoxStudios.CommunityProject.EditorExtensions.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BoundfoxStudios.CommunityProject.Editor
{
	public static class PrefabManager
	{
		private static AssetLocator<PrefabManagerSO> _assetLocator = new("ScriptableObjects/PrefabManager.asset");

		public static async UniTask SafeInvokeAsync(Action<PrefabManagerSO> callback) =>
			await _assetLocator.SafeInvokeAsync(callback);
	}
}
