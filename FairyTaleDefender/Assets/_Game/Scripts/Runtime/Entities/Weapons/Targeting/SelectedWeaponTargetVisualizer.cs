using System.Collections.Generic;
using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Weapons.Targeting
{
	[AddComponentMenu(Constants.MenuNames.Weapons + "/" + nameof(SelectedWeaponTargetVisualizer))]
	public class SelectedWeaponTargetVisualizer : MonoBehaviour
	{
		[field: SerializeField]
		private WeaponSelectedEventChannelSO WeaponSelectedEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO WeaponDeselectedEventChannel { get; set; } = default!;

		[field: SerializeField]
		private GameObject TargetVisualizationPrefab { get; set; } = default!;

		private readonly List<GameObject> _visualizations = new();
		private ICanTrackTarget? _currentTargetTracker;

		private void OnEnable()
		{
			WeaponSelectedEventChannel.Raised += TryToAttachToTrackableTarget;
			WeaponDeselectedEventChannel.Raised += TryToDetachFromTrackableTarget;
		}

		private void OnDisable()
		{
			WeaponSelectedEventChannel.Raised -= TryToAttachToTrackableTarget;
			WeaponDeselectedEventChannel.Raised -= TryToDetachFromTrackableTarget;
		}

		private void TryToAttachToTrackableTarget(WeaponSelectedEventChannelSO.EventArgs args)
		{
			// We always need to remove existing visualizations when attaching to a new trackable target.
			// If we select one tower and then another tower, there won't be a deselected event, but only a selected one.
			RemoveVisualizations();

			var targetTracker = args.Transform.GetComponentInChildren<ICanTrackTarget>();

			if (targetTracker is null)
			{
				return;
			}

			_currentTargetTracker = targetTracker;
			_currentTargetTracker.TargetChanged += TargetChanged;


			if (targetTracker.Target.Exists())
			{
				AddVisualizations(targetTracker.Target);
			}
		}

		private void TryToDetachFromTrackableTarget()
		{
			RemoveVisualizations();

			if (_currentTargetTracker != null)
			{
				_currentTargetTracker.TargetChanged -= TargetChanged;
			}

			_currentTargetTracker = null;
		}

		private void TargetChanged()
		{
			RemoveVisualizations();

			// It could be that we're changing the target, but it was just destroyed.
			if (_currentTargetTracker != null && _currentTargetTracker.Target.Exists())
			{
				AddVisualizations(_currentTargetTracker.Target);
			}
		}

		private void RemoveVisualizations()
		{
			var existingVisualizations = _visualizations.Where(v => v.Exists()).ToArray();

			foreach (var visualization in existingVisualizations)
			{
				Destroy(visualization);
			}

			_visualizations.Clear();
		}

		private void AddVisualizations(TargetPoint targetPoint)
		{
			var enemyTransform = targetPoint.Enemy.transform;

			var visualization = Instantiate(TargetVisualizationPrefab, enemyTransform);
			_visualizations.Add(visualization);
		}
	}
}
