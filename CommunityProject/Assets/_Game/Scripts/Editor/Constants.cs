using RuntimeConstants = BoundfoxStudios.CommunityProject.Constants;

namespace BoundfoxStudios.CommunityProject.Editor
{
	public static class Constants
	{
		public static class MenuNames
		{
			public const string MenuName = RuntimeConstants.MenuNames.MenuName;

			public static class GameObjectMenus
			{
				public const string UI = GameObjectRoot + "/UI";
				public const string Texts = UI + "/Texts";

				public const string Cameras = GameObjectRoot + "/Cameras";
				public const string Editor = GameObjectRoot + "/Editor";

				// This must be "GameObject" to integrate into the Unity GameObject menu.
				private const string GameObjectRoot = "GameObject/" + MenuName;
			}
		}
	}
}
