using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Systems.NavigationSystem;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.Splines;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Splines;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Editors.NavigationSystem
{
	[CustomEditor(typeof(WaySplineCreator))]
	public class WaySplineCreatorEditor : UnityEditor.Editor
	{
		private MethodInfo? _reverseSplineFlow;

		private void OnEnable()
		{
			FindReverseSplineFlowMethod();
		}

		private void FindReverseSplineFlowMethod()
		{
			var type = typeof(EditorSplineUtility);
			_reverseSplineFlow = type.GetMethod("ReverseFlow", BindingFlags.NonPublic | BindingFlags.Static);

			if (_reverseSplineFlow == null)
			{
				Debug.LogError("Did not find ReverseFlow method in EditorSplineUtility.");
			}
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			var waySplineCreator = (WaySplineCreator)target;

			if (!waySplineCreator.SplineContainer
			    || !waySplineCreator.WayContainer
			    || !waySplineCreator.StartTile
			    || !waySplineCreator.EndTile
			   )
			{
				EditorGUILayout.HelpBox("Please assign all fields first", MessageType.Error);
				return;
			}

			if (GUILayout.Button("Create Way Spline"))
			{
				CreateWaySpline(
					waySplineCreator.SplineContainer,
					waySplineCreator.WayContainer,
					waySplineCreator.StartTile,
					waySplineCreator.EndTile
				);
			}
		}

		private void CreateWaySpline(
			SplineContainer splineContainer,
			GameObject wayContainer,
			GameObject startTile,
			GameObject endTile)
		{
			var startKnots = GetKnots(GetSplineInfos(GetSplineContainer(startTile)));
			var endKnots = GetKnots(GetSplineInfos(GetSplineContainer(endTile)));

			if (startKnots.Count > 1)
			{
				throw new NotSupportedException("Having more than one spline for the start tile is not supported.");
			}

			if (endKnots.Count > 1)
			{
				throw new NotSupportedException("Having more than one spline for the end tile is not supported.");
			}

			var splineKnots = GetKnots(wayContainer.GetComponentsInChildren<SplineContainer>()
				.SelectMany(GetSplineInfos));

			EditorUtility.SetDirty(splineContainer);

			ClearSplineContainer(splineContainer);

			var spline = new Spline();

			var start = startKnots.First();
			var currentKnots = start.Value;
			var currentSpline = start.Key;

			var end = endKnots.Last();
			var finalKnot = end.Value.Last();

			InsertKnots(spline, currentKnots);

			// Fail-safe
			var maxIterations = splineKnots.Sum(kvp => kvp.Value.Count);
			var iteration = 0;

			do
			{
				var endKnot = currentKnots.Last();
				var possibleFound = FindKnot(splineKnots, endKnot, currentSpline);

				// We possibly reached the end.
				if (!possibleFound.HasValue)
				{
					if (finalKnot.Knot.IsCloseTo(endKnot.Knot))
					{
						// we reached the end, everything is ok!
						break;
					}

					throw new("End of spline reached, but it seems we did knot find the real end knot!");
				}

				var (foundSpline, knotsToAdd, index) = possibleFound.Value;

				// If the index is not zero, we need to reverse the spline, because the tile may be rotated.
				if (index != 0)
				{
					// Doing some magic to reverse the spline flow via code.
					var fakeSplineContainer = new FakeSplineContainer();
					var splineCopy = new Spline(foundSpline.Spline);
					fakeSplineContainer.AddSpline(splineCopy);

					var splineInfoCopy = new SplineInfo(fakeSplineContainer, 0);
					_reverseSplineFlow?.Invoke(null, new object[] { splineInfoCopy });

					knotsToAdd = GetKnots(new[] { splineInfoCopy }, foundSpline.LocalToWorld).First().Value;
					// knotsToAdd.Reverse();
				}

				// Skip the first knot in the list, because it's the same as we've already added.
				InsertKnots(spline, knotsToAdd.Skip(1));

				currentKnots = knotsToAdd;
				currentSpline = foundSpline;

				iteration++;
			} while (iteration < maxIterations);

			splineContainer.AddSpline(spline);
		}

		private void ClearSplineContainer(SplineContainer splineContainer)
		{
			for (var i = splineContainer.Splines.Count - 1; i >= 0; i--)
			{
				splineContainer.RemoveSplineAt(i);
			}
		}

		private (SplineInfo SplineInfo, List<SplineKnot> Knots, int Index)? FindKnot(
			Dictionary<SplineInfo, List<SplineKnot>> knots,
			SplineKnot knotToFind, SplineInfo ignoreSpline)
		{
			// Remove the ignoreSpline because that one will also contain the knot, we're looking for.
			// But we're not interested in that one, because otherwise we would never proceed forwards.
			var allKnots = knots.Where(kvp => kvp.Key.Spline != ignoreSpline.Spline);

			// If this one throws, we run into a scenario, where there is more than one spline containing the same knot.
			// It could be, that in the level two tiles overlap the same position.
			// If that is not the case, we run into a not supported scenario and take a closer look.
			var (splineInfo, splineKnots) =
				allKnots.SingleOrDefault(kvp => kvp.Value.Any(k => k.Knot.IsCloseTo(knotToFind.Knot)));

			if (!splineInfo.Object)
			{
				return null;
			}

			var splineKnotIndex = splineKnots.FindIndex(knot => knot.Knot.IsCloseTo(knotToFind.Knot));

			return (splineInfo, splineKnots, splineKnotIndex);
		}

		private SplineContainer GetSplineContainer(GameObject gameObject) =>
			gameObject.GetComponentInChildren<SplineContainer>();

		private IReadOnlyList<SplineInfo> GetSplineInfos(SplineContainer splineContainer)
		{
			var result = splineContainer.Splines
				.Select((_, index) => new SplineInfo(splineContainer, index))
				.ToList();

			Assert.IsTrue(result.Count == 1, "Found more than one Spline in a SplineContainer. This is not supported yet.");

			return result;
		}

		private Dictionary<SplineInfo, List<SplineKnot>> GetKnots(IEnumerable<SplineInfo> splines, float4x4? matrix = null) =>
			splines.ToDictionary(splineInfo => splineInfo, splineInfo => splineInfo.Spline.Knots.Select((knot, i) =>
				new SplineKnot()
				{
					Knot = knot.Transform(matrix ?? splineInfo.LocalToWorld),
					TangentMode = splineInfo.Spline.GetTangentMode(i),
					Tension = splineInfo.Spline.GetAutoSmoothTension(i)
				}).ToList());

		private void InsertKnots(Spline spline, IEnumerable<SplineKnot> knots)
		{
			foreach (var knot in knots)
			{
				InsertKnot(spline, knot);
			}
		}

		private void InsertKnot(Spline spline, SplineKnot knot)
		{
			spline.Add(knot.Knot, knot.TangentMode, knot.Tension);
		}

		struct SplineKnot
		{
			public BezierKnot Knot;
			public TangentMode TangentMode;
			public float Tension;
		}

		private class FakeSplineContainer : ISplineContainer
		{
			public IReadOnlyList<Spline> Splines { get; set; } = new List<Spline>();
			public KnotLinkCollection KnotLinkCollection { get; } = new();
		}
	}
}
