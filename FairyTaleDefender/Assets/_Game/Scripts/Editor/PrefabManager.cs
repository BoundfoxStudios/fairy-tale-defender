using System;
using BoundfoxStudios.FairyTaleDefender.EditorExtensions.ScriptableObjects;
using Cysharp.Threading.Tasks;

namespace BoundfoxStudios.FairyTaleDefender.Editor
{
	public static class PrefabManager
	{
		private static readonly AssetLocator<PrefabManagerSO> AssetLocator = new("ScriptableObjects/PrefabManager.asset");

		public static async UniTask SafeInvokeAsync(Action<PrefabManagerSO> callback) =>
			await AssetLocator.SafeInvokeAsync(callback);
	}
}
