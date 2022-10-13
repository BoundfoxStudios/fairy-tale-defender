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
		[Range(0,100)]
		public float ScrollTextSpeed = 20;
		[Range(0,1000)]
		public int ScrollTextPadding = 100;
		private float screenSizeWidth;
		private float screenSizeHeigth;
		private List<CreditItem> _creditItems = new List<CreditItem>();

        // Start is called before the first frame update
        async void Awake()
        {
			var contibutersReader = new ContributorsReader();
			var _contributers = await contibutersReader.LoadAsync();

			screenSizeWidth = GetComponent<Canvas>().renderingDisplaySize.x;
			screenSizeHeigth = GetComponent<Canvas>().renderingDisplaySize.y;

			_creditItems.Add(new CreditItem(TextMeshProObject, transform, screenSizeWidth, "Boundfox Studios",""));
			_creditItems.Add(new CreditItem(TextMeshProObject, transform, screenSizeWidth,"Community-Projekt",""));
			_creditItems.Add(new CreditItem(TextMeshProObject, transform, screenSizeWidth,"YouTube: https://youtube.com/c/boundfox","https://youtube.com/c/boundfox"));
			_creditItems.Add(new CreditItem(TextMeshProObject, transform, screenSizeWidth,"GitHub: https://github.com/boundfoxstudios/community-project","https://github.com/boundfoxstudios/community-project"));

			foreach(var item in _contributers)
			{
				_creditItems.Add(new CreditItem(TextMeshProObject, transform, screenSizeWidth, item.User,item.Url));
			}

			CalcAllCreditItemPosition();
		}
        // Update is called once per frame
        void Update()
		{
			foreach (var item in _creditItems)
			{
				item.SetHorizontalPosition(item.GetHorizontalPosition() + Time.deltaTime * 500.0f * (ScrollTextSpeed / 100.0f));
			}

			if (_creditItems.Count > 0)
			{
				if (_creditItems[_creditItems.Count - 1].GetHorizontalPosition() > screenSizeHeigth)
					CalcAllCreditItemPosition();
			}
		}

		private void CalcAllCreditItemPosition()
		{
			float startPosition = -150.0f;
			foreach(var item in _creditItems)
			{
				item.SetHorizontalPosition(startPosition);
				startPosition -= ScrollTextPadding;
			}
		}
    }
}
