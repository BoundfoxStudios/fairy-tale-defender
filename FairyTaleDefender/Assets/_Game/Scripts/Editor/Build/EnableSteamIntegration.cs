using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Build
{
	public class EnableSteamIntegration : IPreprocessBuildWithReport
	{
		public int callbackOrder { get; }

		public void OnPreprocessBuild(BuildReport report)
		{
			var enableSteamEnvironmentVariable = Environment.GetEnvironmentVariable(Constants.CompilerDirectives.EnableSteam);
			var enableSteam = (enableSteamEnvironmentVariable ?? "false").ToLower() == "true";

			Debug.Log(
				$"{Constants.CompilerDirectives.EnableSteam}: {enableSteam} (environment variable raw value: {enableSteamEnvironmentVariable})");

			if (!enableSteam)
			{
				return;
			}

			PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
				out var defines);

			var uniqueDefines = new HashSet<string>(defines) { Constants.CompilerDirectives.EnableSteam };

			PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
				uniqueDefines.ToArray());
		}
	}
}
