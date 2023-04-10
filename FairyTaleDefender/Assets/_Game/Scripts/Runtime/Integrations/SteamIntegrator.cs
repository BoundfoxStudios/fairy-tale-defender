using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Common.Integrations.Steam;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using UnityEngine;

#if ENABLE_STEAM
using BoundfoxStudios.FairyTaleDefender.Integrations.Steam;
#endif

namespace BoundfoxStudios.FairyTaleDefender
{
	[AddComponentMenu(Constants.MenuNames.SteamIntegration + "/" + nameof(SteamIntegrator))]
	public class SteamIntegrator : MonoBehaviour
	{
		[field: SerializeField]
		public SteamRuntimeAnchorSO SteamRuntimeAnchor { get; private set; } = default!;

		private void Awake()
		{
			Integrate();
		}

		private void Integrate()
		{
#if ENABLE_STEAM
			var steamGameObject = new GameObject(nameof(SteamManager), typeof(SteamManagerImpl));
#else
			var steamGameObject = new GameObject(nameof(SteamManager), typeof(SteamManager));
#endif

			steamGameObject.transform.SetParent(transform);

			var steamManager = steamGameObject.GetComponent<SteamManager>();
			steamManager.Initialize();
			SteamRuntimeAnchor.Item = steamManager;
		}
	}
}
