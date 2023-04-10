using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Common.Integrations.Steam
{
	/// <summary>
	/// This file contains the <see cref="SteamManager"/> to define the API for the real implementation called "SteamManagerImpl".
	/// If the Steam integration is turned of, this mock implementation is used.
	/// We can use it to define mock data that is used during the development of the game.
	/// When the game is being built, we'll enable the real Steam integration and get real data.
	///
	/// If you want to test the Steam integration locally, you can enable the Steam integration via the Unity menu:
	/// "Fairy Tale Defender" -> Windows -> "Steam integration".
	///
	/// To actually use the SteamManager in the game, please use the SteamRuntimeAnchor that will hold an instance
	/// of the correct class.
	/// </summary>
	[AddComponentMenu("")] // Hide in component menu.
	public class SteamManager : MonoBehaviour
	{
		public virtual SteamApps SteamApps { get; } = new();

		public virtual void Initialize()
		{
			Debug.Log("Steam integration has been turned off. To enable go to Fairy Tale Defender -> Windows -> Steam integration");
		}
	}
}
