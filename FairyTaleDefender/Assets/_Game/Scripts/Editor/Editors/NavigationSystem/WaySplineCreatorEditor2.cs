using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Debug = UnityEngine.Debug;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Editors.NavigationSystem
{
	[CustomEditor(typeof(WaySplineCreator))]
	public class WaySplineCreatorEditor2 : UnityEditor.Editor
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

			if (GUILayout.Button("Create Way Spline 2"))
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
			var startSplineContainer = GetSplineContainer(startTile);
			var endSplineContainer = GetSplineContainer(endTile);

			startSplineContainer.ThrowIfMoreThanOneSpline();
			endSplineContainer.ThrowIfMoreThanOneSpline();

			var allSplineContainers = wayContainer.GetComponentsInChildren<SplineContainer>();
			var allSplineInfos = allSplineContainers.SelectMany(GetSplineInfos);
			var allKnots = allSplineInfos.SelectMany(splineInfo => splineInfo.GetExtendedKnots()).ToArray();

			var spline = new Spline();

			EditorUtility.SetDirty(splineContainer);
			splineContainer.Clear();

			var currentSplineInfo = GetSplineInfos(startSplineContainer)[0];
			var nextKnot = currentSplineInfo.GetExtendedKnot(0);
			var endKnot = GetSplineInfos(endSplineContainer)[0].GetExtendedKnots().Last();

			const int maxIterations = 1000;
			var iteration = 0;

			do
			{
				var knots = TraverseSplineInfo(currentSplineInfo, nextKnot);

				if (knots.Count == 0)
				{
					Debug.LogError("No knots found?!");
					break;
				}

				if (nextKnot.Index != 0)
				{
					knots = ReverseSplineFlow(knots.First().SplineInfo);
				}

				// Skip first knot if we are not on the start tile
				spline.InsertExtendedKnots(knots.Skip(spline.Count > 0 ? 1 : 0));
				var possibleNextKnot = GetNextKnot(allKnots, knots.Last());

				if (possibleNextKnot is null)
				{
					if (nextKnot.SplineInfo.GetExtendedKnot(nextKnot.SplineInfo.Spline.Count - 1).Knot.IsCloseTo(endKnot.Knot))
					{
						// we reached the end knot, everything went ok!
						break;
					}

					Debug.LogError($"{nameof(possibleNextKnot)} is null, that should not happen.", nextKnot.SplineInfo.Object);
					break;
				}

				nextKnot = possibleNextKnot;
				currentSplineInfo = nextKnot.SplineInfo;

				if (spline.Count > 100)
				{
					Debug.LogError($"Spline count bigger 100 after {iteration} iterations");
					break;
				}

				iteration++;
			} while (iteration < maxIterations);

			splineContainer.AddSpline(spline);
		}

		private IList<ExtendedKnot> ReverseSplineFlow(SplineInfo splineInfo)
		{
			// Doing some magic to reverse the spline flow via code.
			var fakeSplineContainer = new FakeSplineContainer();
			var splineCopy = new Spline(splineInfo.Spline);
			fakeSplineContainer.AddSpline(splineCopy);

			var splineInfoCopy = new SplineInfo(fakeSplineContainer, 0);
			_reverseSplineFlow?.Invoke(null, new object[] { splineInfoCopy });

			return splineInfoCopy
				.GetExtendedKnots(splineInfo.LocalToWorld)
				.Select(knot =>
				{
					knot.SplineInfo = splineInfo;
					return knot;
				})
				.ToArray();
		}

		private IList<ExtendedKnot> TraverseSplineInfo(SplineInfo splineInfo, ExtendedKnot startKnot)
		{
			var knots = splineInfo.GetExtendedKnots();
			var knotLinkCollection = splineInfo.Container.KnotLinkCollection;

			// If there are no knot links we can immediately return the whole spline.
			if (knotLinkCollection.Count == 0)
			{
				return knots;
			}

			// startKnot.SplineInfo.Index

			// var knotLinks = knotLinkCollection.GetKnotLinks();

			Debug.LogError("Not supported yet!");
			return Array.Empty<ExtendedKnot>();
		}

		private ExtendedKnot? GetNextKnot(ExtendedKnot[] allKnots, ExtendedKnot knotToFind) =>
			allKnots.SingleOrDefault(extendedKnot =>
				extendedKnot.Knot.IsCloseTo(knotToFind.Knot) && extendedKnot.SplineInfo.Spline != knotToFind.SplineInfo.Spline);

		private SplineContainer GetSplineContainer(GameObject gameObject) =>
			gameObject.GetComponentInChildren<SplineContainer>();

		private IList<SplineInfo> GetSplineInfos(ISplineContainer splineContainer) =>
			splineContainer.Splines.Select((_, index) => new SplineInfo(splineContainer, index)).ToArray();


		private class FakeSplineContainer : ISplineContainer
		{
			public IReadOnlyList<Spline> Splines { get; set; } = new List<Spline>();
			public KnotLinkCollection KnotLinkCollection { get; } = new();
		}
	}

	[DebuggerDisplay("Pos: {Knot.Position.xz}; Index: {Index}; Spline: {PossibleSplineContainerName}")]
	internal class ExtendedKnot
	{
		public SplineInfo SplineInfo;
		public BezierKnot Knot;
		public int Index;
		public TangentMode TangentMode;
		public float Tension;

		private string PossibleSplineContainerName => SplineInfo.Object ? SplineInfo.Object.name : "None";
	}

	internal static class Extensions
	{
		public static ExtendedKnot GetExtendedKnot(this SplineInfo splineInfo, int index, float4x4? matrix = null) => new()
		{
			SplineInfo = splineInfo,
			Knot = splineInfo.Spline[index].Transform(matrix ?? splineInfo.LocalToWorld),
			Index = index,
			TangentMode = splineInfo.Spline.GetTangentMode(index),
			Tension = splineInfo.Spline.GetAutoSmoothTension(index)
		};

		public static IList<ExtendedKnot> GetExtendedKnots(this SplineInfo splineInfo, float4x4? matrix = null) =>
			splineInfo.Spline.Select((_, index) => splineInfo.GetExtendedKnot(index, matrix)).ToArray();

		public static void InsertExtendedKnots(this Spline spline, IEnumerable<ExtendedKnot> extendedKnots)
		{
			foreach (var extendedKnot in extendedKnots)
			{
				spline.Add(extendedKnot.Knot, extendedKnot.TangentMode, extendedKnot.Tension);
			}
		}

		public static void ThrowIfMoreThanOneSpline(this SplineContainer splineContainer)
		{
			if (splineContainer.Splines.Count > 1)
			{
				Debug.LogError("Click me to know which SplineContainer has more than one spline", splineContainer);
				throw new(
					$"SplineContainer {splineContainer.name} is only allowed to have one Spline, but has {splineContainer.Splines.Count}");
			}
		}

		public static void Clear(this ISplineContainer splineContainer)
		{
			for (var i = splineContainer.Splines.Count - 1; i >= 0; i--)
			{
				splineContainer.RemoveSplineAt(i);
			}
		}
	}
}
