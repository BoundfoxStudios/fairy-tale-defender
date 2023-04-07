using BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	/// <summary>
	/// Event that can be used whenever a scene needs to be loaded, e.g. <see cref="MenuSO"/>.
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Load Scene Event Channel")]
	public class LoadSceneEventChannelSO : EventChannelSO<LoadSceneEventChannelSO.EventArgs>
	{
		public struct EventArgs
		{
			public SceneSO Scene;
			public bool ShowLoadingScreen;

			public override string ToString() => $"{nameof(Scene)}={Scene.name}, {nameof(ShowLoadingScreen)}={ShowLoadingScreen.ToString()}";
		}
	}
}
