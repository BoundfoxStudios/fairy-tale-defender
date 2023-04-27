using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BoundfoxStudios.FairyTaleDefender.UI.MainMenu
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(ExitButton))]
	public class ExitButton : MonoBehaviour
	{
		public void Exit()
		{
#if UNITY_EDITOR
			EditorApplication.ExitPlaymode();
#else
			Application.Quit();
#endif
		}
	}
}
