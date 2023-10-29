using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.SettingsSystem.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI.Settings
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(ResolutionsSetting))]
	public class ResolutionsSetting : SettingBase
	{
		[field: SerializeField]
		private TMP_Dropdown ResolutionsDropdown { get; set; } = default!;

		private void Awake()
		{
			ResolutionsDropdown.ClearOptions();

			var options = Screen.resolutions
				.GroupBy(resolution => new { resolution.width, resolution.height })
				.Select(resolutionGroup =>
				{
					var resolution = resolutionGroup.First();
					return CreateResolution(resolution.width, resolution.height);
				})
				.Reverse()
				.ToList();

			ResolutionsDropdown.AddOptions(options);
		}

		private string CreateResolution(int width, int height) => $"{width}x{height}";

		public override void ResetSettings(SettingsSO mutableSettings)
		{
			base.ResetSettings(mutableSettings);

			ResolutionsDropdown.SetValueWithoutNotify(
				ResolutionsDropdown.options.FindIndex(item =>
					item.text == CreateResolution(mutableSettings.Graphic.ScreenWidth, mutableSettings.Graphic.ScreenHeight)));
		}

		public void DropdownChange(int value)
		{
			var resolution = ResolutionsDropdown.options[value].text.Split("x");
			var width = int.Parse(resolution[0]);
			var height = int.Parse(resolution[1]);

			MutableSettings.Graphic.ScreenWidth = width;
			MutableSettings.Graphic.ScreenHeight = height;
			OnSettingsChange();
		}
	}
}
