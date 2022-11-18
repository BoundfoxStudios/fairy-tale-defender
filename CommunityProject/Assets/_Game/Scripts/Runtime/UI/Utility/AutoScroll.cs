using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.CommunityProject.UI.Utility
{
	[RequireComponent(typeof(ScrollRect))]
	public class AutoScroll : MonoBehaviour
	{
		private ScrollRect _scrollRect;

		[SerializeField] private float Scrollspeed = 1;

		private void Awake()
		{
			_scrollRect = GetComponent<ScrollRect>();
		}

		// Update is called once per frame
		void Update()
		{
			_scrollRect.verticalNormalizedPosition -= Scrollspeed * Time.deltaTime;

			if (_scrollRect.verticalNormalizedPosition <= 0)
			{
				_scrollRect.verticalNormalizedPosition = 1;
			}
		}
	}
}
