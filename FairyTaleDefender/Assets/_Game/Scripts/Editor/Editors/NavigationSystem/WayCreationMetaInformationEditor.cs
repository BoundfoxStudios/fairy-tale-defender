using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Systems.NavigationSystem;
using UnityEditor;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Editors.NavigationSystem
{
	[CustomEditor(typeof(WayCreationMetaInformation))]
	public class WayCreationMetaInformationEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			EditorGUILayout.HelpBox("Please don't change the exit table, if you don't know, what it is.", MessageType.Info);

			base.OnInspectorGUI();

			var metaInformation = (WayCreationMetaInformation)target;

			if (metaInformation.HasExit)
			{
				DrawExitInspector(metaInformation);
			}
		}

		private void DrawExitInspector(WayCreationMetaInformation metaInformation)
		{
			var splineContainer = metaInformation.SplineContainer;

			if (splineContainer is null)
			{
				EditorGUILayout.HelpBox($"Please assign {nameof(metaInformation.SplineContainer)} first", MessageType.Warning);
				return;
			}

			var options = metaInformation.Exits.Select((_, index) => $"Exit {index + 1}").ToArray();
			metaInformation.ExitIndex = EditorGUILayout.Popup("Exit to use", metaInformation.ExitIndex, options);
		}
	}
}
