using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI.MainMenu
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(SponsorButtons))]
	public class SponsorButtons : MonoBehaviour
	{
		public void OpenJetBrains()
		{
			Application.OpenURL("https://jetbrains.com");
		}

		public void OpenTeamCity()
		{
			Application.OpenURL("https://boundfoxstudios.teamcity.com/");
		}
	}
}
