using BoundfoxStudios.CommunityProject.Build.Contributors;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.CommunityProject.UI
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(ViewCredits))]
    public class ViewCredits : MonoBehaviour
    {
		public GameObject TextMeshProObject;

		private float screenSizeWidth;
		private float screenSizeHeigth;

		private List<string> _creditsList = new ();
		private GameObject go;

		private float pos = -150.0f;
		
		[Range(0,100)]
		public float ScrollTextSpeed = 0;

        // Start is called before the first frame update
        async void Awake()
        {
			var contibutersReader = new ContributorsReader();
			var contributers = await contibutersReader.LoadAsync();

			screenSizeWidth = GetComponent<Canvas>().renderingDisplaySize.x;
			screenSizeHeigth = GetComponent<Canvas>().renderingDisplaySize.y;
			screenSizeHeigth = Screen.height;

			var canvas = GetComponent<Canvas>();

			// Neues GO für Text erstellen 
			go = Instantiate(TextMeshProObject, new Vector3(0, 0, 0.0f), Quaternion.identity);

			go.transform.parent = transform;
			go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, screenSizeHeigth / 2);
			go.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, screenSizeWidth);

			Debug.Log("Contributer Count: " + contributers.LongLength);

			go.GetComponent<TMPro.TextMeshProUGUI>().text = contributers[0].User;
			go.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f , 0.0f);
			go.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f , 0.0f);
		}
        // Update is called once per frame
        void Update()
        {
			float a = Time.deltaTime;

			pos += a * 500f * ( ScrollTextSpeed / 100.0f);

			if(pos > screenSizeHeigth)
				pos = -150.0f;

			go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, pos);
        }

		void FillCreditList()
		{

		}
    }
}
