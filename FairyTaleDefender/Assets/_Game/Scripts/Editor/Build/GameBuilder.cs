using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Build
{
	public static class GameBuilder
	{
		private const int MenuPriority = 100000;
		private static readonly string[] Scenes = { "Assets/_Game/Scenes/Managers/Initialization.unity" };

		#region macOS

		[MenuItem(Constants.MenuNames.Build + "/macOS (Debug)", priority = MenuPriority)]
		public static void DebugBuildMacOS() => BuildMacOS(true);

		[MenuItem(Constants.MenuNames.Build + "/macOS (Release)", priority = MenuPriority)]
		public static void ReleaseBuildMacOS() => BuildMacOS();

		private static void BuildMacOS(bool isDebug = false) =>
			Build(BuildTarget.StandaloneOSX, "FairyTaleDefender.app", isDebug);

		#endregion

		#region Windows

		[MenuItem(Constants.MenuNames.Build + "/Windows (Debug)", priority = MenuPriority)]
		public static void DebugBuildWindows() => BuildWindows(true);

		[MenuItem(Constants.MenuNames.Build + "/Windows (Release)", priority = MenuPriority)]
		public static void ReleaseBuildWindows() => BuildWindows();

		private static void BuildWindows(bool isDebug = false) =>
			Build(BuildTarget.StandaloneWindows64, "FairyTaleDefender", isDebug: isDebug);

		#endregion

		#region Linux

		[MenuItem(Constants.MenuNames.Build + "/Linux (Debug)", priority = MenuPriority)]
		public static void DebugBuildLinux() => BuildLinux(true);

		[MenuItem(Constants.MenuNames.Build + "/Linux (Release)", priority = MenuPriority)]
		public static void ReleaseBuildLinux() => BuildLinux();

		private static void BuildLinux(bool isDebug = false) =>
			Build(BuildTarget.StandaloneLinux64, "FairyTaleDefender", isDebug: isDebug);

		#endregion

		private static void Build(BuildTarget target, string locationName, bool isDebug = false)
		{
			var options = new BuildPlayerOptions()
			{
				scenes = Scenes,
				target = target,
				targetGroup = BuildTargetGroup.Standalone,
				locationPathName = CreateBuildFolderPath(target, locationName),
			};

			if (isDebug)
			{
				options.options = BuildOptions.Development | BuildOptions.AllowDebugging;
			}

			var report = BuildPipeline.BuildPlayer(options);

			if (report.summary.result == BuildResult.Succeeded)
			{
				Debug.Log($"Build success! Total size {report.summary.totalSize / 1024 / 1024:F} MB");
				return;
			}

			Debug.LogError("Build failed!");
		}

		private static string CreateBuildFolderPath(BuildTarget target, string name) =>
			Path.Join("build", target.ToString(), name);
	}
}
