using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

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
				PlayerSettings.bundleVersion = version;
			}
		}
	}
}
