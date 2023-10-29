using System;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI.Settings
{
	public abstract class SettingBase : MonoBehaviour, IAmASetting
	{
		protected SettingsSO MutableSettings = default!;

		public event Action SettingsChange = delegate { };

		public virtual void ResetSettings(SettingsSO mutableSettings)
		{
			MutableSettings = mutableSettings;
		}

		protected void OnSettingsChange() => SettingsChange();
	}
}
