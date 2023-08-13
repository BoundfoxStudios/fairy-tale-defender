using RuntimeConstants = BoundfoxStudios.FairyTaleDefender.Common.Constants;

namespace BoundfoxStudios.FairyTaleDefender.Editor
{
	public static class Constants
	{
		public static class MenuNames
		{
			public const string MenuName = RuntimeConstants.MenuNames.MenuName;
			public const string Windows = MenuName + "/Windows";
			public const string Build = MenuName + "/Build";

			public static class GameObjectMenus
			{
				public const string UI = GameObjectRoot + "/UI";
				public const string Texts = UI + "/Texts";
				public const string Bars = UI + "/Bars";
				public const string Buttons = UI + "/Buttons";

				public const string Cameras = GameObjectRoot + "/Cameras";
				public const string Lights = GameObjectRoot + "/Lights";
				public const string Editor = GameObjectRoot + "/Editor";

				// This must be "GameObject" to integrate into the Unity GameObject menu.
				private const string GameObjectRoot = "GameObject/" + MenuName;
			}
		}

		public static class CompilerDirectives
		{
			public const string EnableSteam = "ENABLE_STEAM";
		}
	}
}
