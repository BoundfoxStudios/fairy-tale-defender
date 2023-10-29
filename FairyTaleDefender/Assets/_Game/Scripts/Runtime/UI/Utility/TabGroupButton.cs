using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI.Utility
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(TabGroupButton))]
	public class TabGroupButton : MonoBehaviour, IToggleButtonAction
	{
		private TabGroup _tabGroup = default!;

		private void Awake()
		{
			_tabGroup = GetComponentInParent<TabGroup>();

			Debug.Assert(_tabGroup is not null);
		}

		public void ExecuteAction(int index) => _tabGroup.Show(index);
	}
}
