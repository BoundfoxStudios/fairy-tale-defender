using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.FairyTaleDefender.UI.Settings
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(EdgePanningSetting))]
	public class EdgePanningSetting : SettingBase
	{
		[field: SerializeField]
		private Toggle Toggle { get; set; } = default!;

		public override void ResetSettings(SettingsSO mutableSettings)
		{
			base.ResetSettings(mutableSettings);

			Toggle.SetIsOnWithoutNotify(mutableSettings.Camera.EnableEdgePanning);
		}

		public void ToggleChange(bool value)
		{
			MutableSettings.Camera.EnableEdgePanning = value;
			OnSettingsChange();
		}
	}
}
