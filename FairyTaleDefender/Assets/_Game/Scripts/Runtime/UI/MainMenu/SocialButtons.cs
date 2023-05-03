using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI.MainMenu
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(SocialButtons))]
	public class SocialButtons : MonoBehaviour
	{
		public void OpenDiscord()
		{
			Application.OpenURL(Constants.SocialLinks.Discord);
		}

		public void OpenYouTube()
		{
			Application.OpenURL(Constants.SocialLinks.FairyTaleDefenderYouTubePlaylist);
		}

		public void OpenGitHub()
		{
			Application.OpenURL(Constants.SocialLinks.GitHub);
		}
	}
}
