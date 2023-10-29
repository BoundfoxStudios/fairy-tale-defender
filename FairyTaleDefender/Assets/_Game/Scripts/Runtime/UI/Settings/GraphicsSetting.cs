using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI.Settings
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(GraphicsSetting))]
	public class GraphicsSetting : SettingBase
	{
		[field: SerializeField]
		private TMP_Dropdown GraphicsDropdown { get; set; } = default!;

		public override void ResetSettings(SettingsSO mutableSettings)
		{
			base.ResetSettings(mutableSettings);

			GraphicsDropdown.SetValueWithoutNotify((int) mutableSettings.Graphic.GraphicLevel);
		}

		public void DropdownChange(int value)
		{
			MutableSettings.Graphic.GraphicLevel = (GraphicLevels)value;
			OnSettingsChange();
		}
	}
}
