using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.BuildSystem
{
	public interface IHaveBuildableUI
	{
		/// <summary>
		/// An image to display in UI e.g. in build buttons.
		/// </summary>
		Sprite BuildableIcon { get; }
	}
}
