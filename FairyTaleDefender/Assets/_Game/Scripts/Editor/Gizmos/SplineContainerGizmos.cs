using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Systems.NavigationSystem;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.Splines;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Gizmos
{
	public static class SplineContainerGizmos
	{
		[DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.Active, typeof(SplineContainerDiagnostics))]
		private static void DrawBallisticWeaponGizmos(SplineContainerDiagnostics splineContainerDiagnostics,
			GizmoType gizmoType)
		{
			if (!splineContainerDiagnostics.IsSpawnPoint)
			{
				return;
			}

			var splineContainer = splineContainerDiagnostics.SplineContainer;

			if (!splineContainer.Exists())
			{
				return;
			}

			DrawArrowForSpline(splineContainer, 0);
		}

		private static void DrawArrowForSpline(SplineContainer splineContainer, int index)
		{
			var spline = new SplineInfo(splineContainer, index);
			var knots = spline.Spline.Knots.ToList();

			if (knots.Count < 2)
			{
				return;
			}

			for (var i = 0; i < knots.Count - 2; i++)
			{
				Debug.DrawLine(
					knots[i].Transform(spline.LocalToWorld).Position,
					knots[i + 1].Transform(spline.LocalToWorld).Position,
					Color.magenta
				);
			}

			ArrowHelper.Draw(knots[^2].Transform(spline.LocalToWorld).Position,
				knots[^1].Transform(spline.LocalToWorld).Position, Color.magenta);
		}
	}
}
