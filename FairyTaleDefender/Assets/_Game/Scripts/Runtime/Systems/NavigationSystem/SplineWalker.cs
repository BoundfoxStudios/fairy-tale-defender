using System;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
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

		public float MovementSpeed { get; set; } = 1;

		public event Action ReachedEndOfSpline = delegate { };

		/// <summary>
		/// Overall duration it will take to traverse <see cref="_spline"/>
		/// </summary>
		private float _duration;

		/// <summary>
		/// 0..1 where 0 is the start of the spline and 1 is the end of the spline.
		/// </summary>
		private float _normalizedTime;

		private float _elapsedTime;
		private ISpline _spline = default!;

		public void Initialize(ISpline spline, float movementSpeed)
		{
			_spline = spline;
			_duration = spline.GetLength() / MovementSpeed;
			MovementSpeed = movementSpeed;
		}

		private void CalculateNormalizedTime(float deltaTime)
		{
			_elapsedTime += deltaTime;

			var t = math.min(_elapsedTime, _duration);
			t /= _duration;

			_normalizedTime = math.floor(_normalizedTime) + t;
		}

		private void FixedUpdate()
		{
			CalculateNormalizedTime(Time.deltaTime * MovementSpeed);

			var (position, rotation) = EvaluateSpline(_normalizedTime);

			Rigidbody.Move(position, rotation);

			if (_normalizedTime > 1)
			{
				ReachedEndOfSpline();
			}
		}

		private (float3 Position, Quaternion Tangent) EvaluateSpline(float normalizedTime)
		{
			WaySplineRuntimeAnchor.ItemSafe.Evaluate(_spline, normalizedTime, out var position, out var tangent, out _);

			var rotation = tangent.Equals(float3.zero) ? Quaternion.identity : Quaternion.LookRotation(tangent);

			return (position, rotation);
		}
	}
}
