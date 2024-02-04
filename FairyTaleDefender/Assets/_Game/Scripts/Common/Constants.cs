using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Common
{
	public static class Constants
	{
		public static class MenuNames
		{
			public const string MenuName = "Fairy Tale Defender";
			public const string Events = MenuName + "/Events";
			public const string EventChannelSuffix = " Event Channel";
			public const string SceneManagement = MenuName + "/Scene Management";
			public const string UI = MenuName + "/UI";
			public const string Input = MenuName + "/Input";
			public const string Audio = MenuName + "/Audio";
			public const string Navigation = MenuName + "/Navigation";
			public const string Buildings = MenuName + "/Buildings";
			public const string Towers = Buildings + "/Towers";
			public const string Characters = MenuName + "/Characters";
			public const string HealthSystem = MenuName + "/Health System";
			public const string Weapons = MenuName + "/Weapons";
			public const string Targeting = Weapons + "/Targeting";
			public const string CameraSystem = MenuName + "/Camera System";
			public const string GameplaySystem = MenuName + "/Gameplay System";
			public const string SpawnSystem = MenuName + "/Spawn System";
			public const string RuntimeAnchors = MenuName + "/Runtime Anchors";
			public const string Integrations = MenuName + "/Integrations";
			public const string SteamIntegration = Integrations + "/Steam";
			public const string BuildSystem = MenuName + "/Build System";
			public const string Infrastructure = MenuName + "/Infrastructure";
			public const string RuntimeSets = MenuName + "/Runtime Sets";
			public const string SaveGame = MenuName + "/Save Game";
			public const string Objectives = MenuName + "/Objectives";
		}

		public static class SocialLinks
		{
			public const string GitHub = "https://github.com/boundfoxstudios/fairy-tale-defender";

			public const string FairyTaleDefenderYouTubePlaylist =
				"https://www.youtube.com/playlist?list=PLxVAs8AY4TgchOtBZqg4qvFeq6w74ZtAm";

			public const string Discord = "https://discord.gg/tHqNzMT";
		}

		public static class AnimationStates
		{
			public static readonly int IsWalking = Animator.StringToHash("IsWalking");
			public static readonly int IsHobbling = Animator.StringToHash("IsHobbling");
		}

		public static class Gameplay
		{
			public const int MaximumTargets = 50;
		}

		public static class SaveGames
		{
			public static readonly string DirectoryName = "Save Games";
			public static readonly string MetaFileName = "meta.json";
			public static readonly string SaveGameFileName = "save.json";
		}

		public static class Settings
		{
			public static class Panning
			{
				public const int Start = 0;
				public const int End = 15;
			}
		}

		public static class FileNames
		{
			public const string EventChannelSuffix = "_EventChannel";
		}
	}
}
