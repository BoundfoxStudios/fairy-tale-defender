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
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
        
		}
		public void OnPointerClick(PointerEventData eventData)
		{
			UnityEngine.Debug.Log("test...");
			Process.Start("www.google.com");
		}
	}
}
