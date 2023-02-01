using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.CommunityProject.UI.Utility
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(AutoScroll))]
	[RequireComponent(typeof(ScrollRect))]
	public class AutoScroll : MonoBehaviour
	{
		[field: SerializeField]
		private float ScrollSpeed { get; set; } = 0.05f;

		private ScrollRect _scrollRect = default!;

		private void Awake()
		{
			_scrollRect = GetComponent<ScrollRect>();
		}

		private void Update()
		{
			_scrollRect.verticalNormalizedPosition -= ScrollSpeed * Time.deltaTime;

			if (_scrollRect.verticalNormalizedPosition <= 0)
			{
				_scrollRect.verticalNormalizedPosition = 1;
			}
		}
	}
}
