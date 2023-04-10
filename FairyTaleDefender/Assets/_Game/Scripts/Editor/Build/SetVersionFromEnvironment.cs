using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Build
{
	public class SetVersionFromEnvironment : IPreprocessBuildWithReport
	{
		public int callbackOrder { get; }

		public void OnPreprocessBuild(BuildReport report)
		{
			var version = Environment.GetEnvironmentVariable("VERSION");

			if (!string.IsNullOrWhiteSpace(version))
			{
				Debug.Log($"Setting version: {version}");
				PlayerSettings.bundleVersion = version;
			}
		}
	}
}
