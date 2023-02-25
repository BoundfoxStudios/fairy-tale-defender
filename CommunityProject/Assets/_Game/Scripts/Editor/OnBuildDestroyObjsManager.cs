using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoundfoxStudios.CommunityProject.Editor
{
	public class OnBuildDestroyObjsManager : IProcessSceneWithReport
	{
		public int callbackOrder { get { return 0; } }
		public void OnProcessScene(Scene scene, BuildReport report)
		{
			if(report == null)return;
			if (!report.summary.options.HasFlag(UnityEditor.BuildOptions.Development))
			{
				GameObject[] objToDestroyOnBuild = GameObject.FindGameObjectsWithTag("DevelopmentOnly");
				int objToDestroyOnBuildLength = objToDestroyOnBuild.Length;
				for (int i = 0; i < objToDestroyOnBuildLength; i++)
				{
					GameObject.DestroyImmediate(objToDestroyOnBuild[i], true);
				}
			}

		}
	}
}
