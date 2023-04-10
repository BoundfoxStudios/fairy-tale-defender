using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

#if ENABLE_STEAM
using BoundfoxStudios.FairyTaleDefender.Integrations.Steam;
#endif

namespace BoundfoxStudios.FairyTaleDefender
{
	[AddComponentMenu(Constants.MenuNames.SteamIntegration + "/" + nameof(SteamIntegrator))]
	public class SteamIntegrator : MonoBehaviour
	{
		private void Awake()
		{
			Integrate();
		}

		private void Integrate()
		{
#if ENABLE_STEAM
			var steamGameObject = new GameObject(nameof(SteamManager), typeof(SteamManager));
			steamGameObject.transform.SetParent(transform);

			var steamManager = steamGameObject.GetComponent<SteamManager>();
			steamManager.Initialize();
#endif
		}
	}
}
