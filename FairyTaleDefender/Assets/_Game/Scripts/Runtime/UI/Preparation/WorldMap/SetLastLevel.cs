using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement;
using BoundfoxStudios.FairyTaleDefender.Systems.SaveGameSystem.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI.Preparation.WorldMap
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(SetLastLevel))]
	public class SetLastLevel : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private SaveGameRuntimeAnchorSO SaveGameRuntimeAnchor { get; set; } = default!;

		[field: SerializeField]
		private SaveGameManagerSO SaveGameManager { get; set; } = default!;

		[field: SerializeField]
		private SceneLoadRequester SceneLoadRequester { get; set; } = default!;

		public void SaveLastLevel()
		{
			SaveGameRuntimeAnchor.ItemSafe.Data.LastLevel = SceneLoadRequester.SceneToLoad;
			SaveGameManager.SaveGameAsync(SaveGameRuntimeAnchor.ItemSafe).Forget();
		}
	}
}
