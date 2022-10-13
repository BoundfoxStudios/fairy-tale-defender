using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.UI
{
    public class CreditItem : MonoBehaviour
    {
		private GameObject creditItemGameObject;
		private float horizontalPosition = 0;
		private string url;
		public CreditItem(GameObject textMeshProObject, Transform transform, float screenSizeWidth, string name, string url)
		{
			
			this.url = url;

			creditItemGameObject = Instantiate(textMeshProObject, new Vector3(0, 0, 0.0f), Quaternion.identity);

			creditItemGameObject.transform.parent = transform;
			creditItemGameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, screenSizeWidth);
			creditItemGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = name;
			creditItemGameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f , 0.0f);
			creditItemGameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f , 0.0f);
			creditItemGameObject.GetComponent<CreditText>().Url = url;
		}

		public void SetHorizontalPosition(float horizontalPosition)
		{
			this.horizontalPosition = horizontalPosition;
			creditItemGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, horizontalPosition);
		}

		public float GetHorizontalPosition()
		{
			return horizontalPosition;
		}
    }
}
