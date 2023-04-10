using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Build
{
	public class StripDevelopmentOnlyObjectsFromBuild : IProcessSceneWithReport
	{
		public int callbackOrder { get; }
		public void OnProcessScene(Scene scene, BuildReport? report)
		{
			if (report is null || report.summary.options.HasFlag(BuildOptions.Development))
			{
				return;
			}
			var objectsToDelete = GameObject.FindGameObjectsWithTag("DevelopmentOnly");
			for (var i = objectsToDelete.Length - 1; i >= 0; i--)
			{
				Object.DestroyImmediate(objectsToDelete[i]);
			}
		}
	}
}
