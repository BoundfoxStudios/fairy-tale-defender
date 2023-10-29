using System;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;

namespace BoundfoxStudios.FairyTaleDefender.UI.Settings
{
	public interface IAmASetting
	{
		event Action SettingsChange;
		void ResetSettings(SettingsSO mutableSettings);
	}
}
