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
			var enableSteamEnvironmentVariable = Environment.GetEnvironmentVariable("ENABLE_STEAM");
			var enableSteam = (enableSteamEnvironmentVariable ?? "false").ToLower() == "true";

			Debug.Log($"ENABLE_STEAM: {enableSteam} (environment variable raw value: {enableSteamEnvironmentVariable})");

			if (!enableSteam)
			{
				return;
			}

			PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
				out var defines);

			var uniqueDefines = new HashSet<string>(defines) { "ENABLE_STEAM" };

			PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
				uniqueDefines.ToArray());
		}
	}
}
