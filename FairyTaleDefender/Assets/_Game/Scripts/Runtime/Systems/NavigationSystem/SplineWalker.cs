using System;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace BoundfoxStudios.FairyTaleDefender.Systems.NavigationSystem
{
	[AddComponentMenu(Constants.MenuNames.Navigation + "/" + nameof(SplineWalker))]
	public class SplineWalker : MonoBehaviour
	{
		[field: SerializeField]
		private Rigidbody Rigidbody { get; set; } = default!;

		[field: SerializeField]
		public WaySplineRuntimeAnchorSO WaySplineRuntimeAnchor { get; set; } = default!;

		[field: SerializeField]
		private Transform Gfx { get; set; } = default!;

		public float MovementSpeed { get; set; } = 1;

		public event Action ReachedEndOfSpline = delegate { };

		/// <summary>
		/// Total length of the given <see cref="_spline"/>
		/// </summary>
		private float _totalDistance;

		/// <summary>
		/// 0..1 where 0 is the start of the spline and 1 is the end of the spline.
		/// </summary>
		private float _normalizedDistance;

		private float _traversedDistance;
		private ISpline _spline = default!;

		public void Initialize(ISpline spline, float movementSpeed)
		{
			_spline = spline;
			MovementSpeed = movementSpeed;
			_totalDistance = spline.GetLength();
		}

		private void CalculateNormalizedDistance(float deltaTime)
		{
			_traversedDistance += deltaTime * MovementSpeed;
			_normalizedDistance = math.clamp((_traversedDistance / _totalDistance), 0, 1);
		}

		private void FixedUpdate()
		{
			CalculateNormalizedDistance(Time.deltaTime);

			var (position, rotation) = EvaluateSpline(_normalizedDistance);

			Rigidbody.Move(position, Quaternion.Euler(0, rotation.eulerAngles.y, 0));

			Gfx.localRotation = Quaternion.Euler(rotation.eulerAngles.x, 0, 0);

			// ReSharper disable once CompareOfFloatsByEqualityOperator
			// Justification: Value is clamped, so we know it will be actually 1.
			if (_normalizedDistance == 1)
			{
				ReachedEndOfSpline();
			}
		}

		private (float3 Position, Quaternion Rotation) EvaluateSpline(float normalizedDistance)
		{
			WaySplineRuntimeAnchor.ItemSafe.Evaluate(_spline, normalizedDistance, out var position, out var tangent, out _);

			var rotation = tangent.Equals(float3.zero) ? Quaternion.identity : Quaternion.LookRotation(tangent);

			return (position, rotation);
		}

		public float GetTimeToReachSplineEnd()
		{
			return (1 - _normalizedDistance) * _totalDistance / MovementSpeed;
		}
	}
}
