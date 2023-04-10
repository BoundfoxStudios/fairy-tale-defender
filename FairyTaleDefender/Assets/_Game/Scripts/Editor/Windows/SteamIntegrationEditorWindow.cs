using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Windows
{
	public class SteamIntegrationEditorWindow : EditorWindow
	{
		private const string EnableSteamIntegrationScriptingDefineSymbol = "ENABLE_STEAM";

		[MenuItem(Constants.MenuNames.Windows + "/Steam Integration", priority = 10000)]
		private static void ShowWindow()
		{
			var window = GetWindow<SteamIntegrationEditorWindow>();
			window.titleContent = new("Steam Integration");
			window.Show();
		}

		private void OnGUI()
		{
			PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
				out var defines);

			var uniqueDefines = new HashSet<string>(defines);

			var isEnabled = uniqueDefines.Contains(EnableSteamIntegrationScriptingDefineSymbol);

			EditorGUILayout.LabelField("Steam is enabled", isEnabled ? "Yes" : "No");

			if (isEnabled)
			{
				EditorGUILayout.HelpBox("Please make sure to turn off the Steam integration before creating a PR!",
					MessageType.Info);
			}

			if (isEnabled && GUILayout.Button("Disable Steam integration"))
			{
				uniqueDefines.Remove(EnableSteamIntegrationScriptingDefineSymbol);
				PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
					uniqueDefines.ToArray());
			}

			if (!isEnabled && GUILayout.Button("Enable Steam integration"))
			{
				uniqueDefines.Add(EnableSteamIntegrationScriptingDefineSymbol);
				PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
					uniqueDefines.ToArray());
			}
		}
	}
}
