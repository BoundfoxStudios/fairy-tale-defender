using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Editor
{
    public class OnBuildDestroyObjsManager : IProcessSceneWithReport, IPreprocessBuildWithReport
	{
		public int callbackOrder { get { return 0; } }
		bool onBuild = false;
		bool isDevelopmentBuild = false;


		public void OnPreprocessBuild(BuildReport report)
		{
			onBuild = true;
			string[] buildOptions = report.summary.options.ToString().Split(',');
			foreach (var option in buildOptions)
			{
				if (option == "developmentbuild")
				{
					isDevelopmentBuild = true;
					return;
				}
			}
		}

		public void OnProcessScene(UnityEngine.SceneManagement.Scene scene, BuildReport report)
		{
			GameObject[] objToDestroyOnBuild = GameObject.FindGameObjectsWithTag("DevelopmentOnly");
			int objToDestroyOnBuildLength = objToDestroyOnBuild.Length;
			for (int i = 0; i < objToDestroyOnBuildLength; i++)
			{
				if (!isDevelopmentBuild && onBuild)
				{
					GameObject.DestroyImmediate(objToDestroyOnBuild[i], true);
				}

			}
		}
	}
}
