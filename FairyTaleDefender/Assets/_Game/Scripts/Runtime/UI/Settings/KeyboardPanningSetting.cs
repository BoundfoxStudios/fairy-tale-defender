using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.FairyTaleDefender.UI.Settings
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(KeyboardPanningSetting))]
	public class KeyboardPanningSetting : SettingBase
	{
		[field: SerializeField]
		private Toggle Toggle { get; set; } = default!;

		public override void ResetSettings(SettingsSO mutableSettings)
		{
			base.ResetSettings(mutableSettings);

			Toggle.SetIsOnWithoutNotify(mutableSettings.Camera.EnableKeyboardPanning);
		}

		public void ToggleChange(bool value)
		{
			MutableSettings.Camera.EnableKeyboardPanning = value;
			OnSettingsChange();
		}
	}
}
