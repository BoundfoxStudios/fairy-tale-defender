using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI.Utility
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(TabGroup))]
	public class TabGroup : MonoBehaviour
	{
		private TabGroupButton[] _buttons = default!;

		[field: SerializeField]
		private GameObject HeaderContainer { get; set; } = default!;

		[field: SerializeField]
		private GameObject BodyContainer { get; set; } = default!;

		private void Awake()
		{
			_buttons = HeaderContainer.GetComponentsInChildren<TabGroupButton>();

			Debug.Assert(_buttons.Length == BodyContainer.transform.childCount);
		}

		public void Show(int index)
		{
			var bodyContainerTransform = BodyContainer.transform;

			for (var i = 0; i < bodyContainerTransform.childCount; i++)
			{
				bodyContainerTransform.GetChild(i).gameObject.SetActive(i == index);
			}
		}
	}
}
