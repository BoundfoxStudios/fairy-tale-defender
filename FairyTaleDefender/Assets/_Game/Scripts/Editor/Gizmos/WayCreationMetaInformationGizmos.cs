using System.Collections.Generic;
using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Editor.Editors.NavigationSystem;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Systems.NavigationSystem;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;
using UnityGizmos = UnityEngine.Gizmos;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Gizmos
{
	public static class WayCreationMetaInformationGizmos
	{
		private static GUIStyle? _exitStyle;
		private static GUIStyle ExitStyle => _exitStyle ??= new(EditorStyles.largeLabel)
		{
			normal =
			{
				 textColor = Color.blue,
			}
		};

		[DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.Active, typeof(WayCreationMetaInformation))]
		private static void DrawGizmos(WayCreationMetaInformation wayCreationMetaInformation,
			GizmoType gizmoType)
		{
			var splineContainer = wayCreationMetaInformation.SplineContainer;

			if (!splineContainer.Exists())
			{
				return;
			}

			HandleExitsDrawing(wayCreationMetaInformation, splineContainer);

			if (wayCreationMetaInformation.IsSpawnPoint)
			{
				DrawSpawnPointArrow(splineContainer, 0);
			}
		}

		private static void HandleExitsDrawing(
			WayCreationMetaInformation wayCreationMetaInformation,
			SplineContainer splineContainer)
		{
			var splineInfos = splineContainer.GetSplineInfos();

			if (wayCreationMetaInformation.Exits.Length > 0)
			{
				DrawExits(wayCreationMetaInformation.Exits, splineInfos);
			}

			if (wayCreationMetaInformation.HasExit)
			{
				DrawSelectedExit(wayCreationMetaInformation.Exits[wayCreationMetaInformation.ExitIndex], splineInfos);
			}
		}

		private static void DrawSelectedExit(SplineKnotIndex exit, IList<SplineInfo> splineInfos)
		{
			var splineInfo = splineInfos[exit.Spline];
			var endPosition = splineInfo.Spline[exit.Knot].Transform(splineInfo.LocalToWorld);

			var offset = new float3(0, 0.1f, 0);
			endPosition.Position += offset;

			var startPosition = splineInfo.Transform.position;
			startPosition.y = endPosition.Position.y;

			ArrowHelper.Draw(startPosition, endPosition.Position, Color.magenta);
		}

		private static void DrawExits(SplineKnotIndex[] exits, IList<SplineInfo> splineInfos)
		{
			for (var index = 0; index < exits.Length; index++)
			{
				var exit = exits[index];
				DrawExit(exit, index, splineInfos);
			}
		}

		private static void DrawExit(SplineKnotIndex exit, int index, IList<SplineInfo> splineInfos)
		{
			var splineInfo = splineInfos[exit.Spline];
			var knot = splineInfo.Spline[exit.Knot].Transform(splineInfo.LocalToWorld);

			Handles.Label(knot.Position, $"Exit {index + 1}", ExitStyle);
		}

		private static void DrawSpawnPointArrow(SplineContainer splineContainer, int index)
		{
			var spline = new SplineInfo(splineContainer, index);
			var knots = spline.Spline.Knots.ToList();

			if (knots.Count < 2)
			{
				return;
			}

			for (var i = 0; i < knots.Count - 2; i++)
			{
				UnityGizmos.color = Color.magenta;
				UnityGizmos.DrawLine(
					knots[i].Transform(spline.LocalToWorld).Position,
					knots[i + 1].Transform(spline.LocalToWorld).Position
				);
			}

			ArrowHelper.Draw(knots[^2].Transform(spline.LocalToWorld).Position,
				knots[^1].Transform(spline.LocalToWorld).Position, Color.magenta);
		}
	}
}
