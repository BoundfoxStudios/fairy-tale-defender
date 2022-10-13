using BoundfoxStudios.CommunityProject.Build.BuildManifest;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BoundfoxStudios.CommunityProject.UI
{
	public class CreditText : MonoBehaviour, IPointerClickHandler
	{
		public string Url = string.Empty;

		public void OnPointerClick(PointerEventData eventData)
		{
			if(Url != string.Empty)	
			{
				Process.Start(Url);
			}
		}
	}
}
