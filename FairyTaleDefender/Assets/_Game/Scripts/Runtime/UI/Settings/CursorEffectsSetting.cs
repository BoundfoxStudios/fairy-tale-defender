using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.FairyTaleDefender.UI.Settings
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(CursorEffectsSetting))]
	public class CursorEffectsSetting : SettingBase
	{
		[field: SerializeField]
		private Toggle Toggle { get; set; } = default!;

		public override void ResetSettings(SettingsSO mutableSettings)
		{
			base.ResetSettings(mutableSettings);

			Toggle.SetIsOnWithoutNotify(mutableSettings.Graphic.EnableCursorEffects);
		}

		public void ToggleChange(bool value)
		{
			MutableSettings.Graphic.EnableCursorEffects = value;
			OnSettingsChange();
		}
	}
}
