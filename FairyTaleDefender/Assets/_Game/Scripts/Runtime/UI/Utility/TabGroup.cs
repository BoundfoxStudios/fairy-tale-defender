using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI.Utility
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(TabGroup))]
	public class TabGroup : MonoBehaviour
	{
		[field: SerializeField]
		private ToggleButtonGroup HeaderToggleButtonGroup { get; set; } = default!;

		[field: SerializeField]
		private GameObject BodyContainer { get; set; } = default!;

		private void Awake()
		{
			Debug.Assert(HeaderToggleButtonGroup.transform.childCount == BodyContainer.transform.childCount);
			Show(0);
		}

		private void OnEnable()
		{
			HeaderToggleButtonGroup.IndexChanged += Show;
		}

		private void OnDisable()
		{
			HeaderToggleButtonGroup.IndexChanged -= Show;
		}

		private void Show(int index)
		{
			var bodyContainerTransform = BodyContainer.transform;

			for (var i = 0; i < bodyContainerTransform.childCount; i++)
			{
				bodyContainerTransform.GetChild(i).gameObject.SetActive(i == index);
			}
		}
	}
}
