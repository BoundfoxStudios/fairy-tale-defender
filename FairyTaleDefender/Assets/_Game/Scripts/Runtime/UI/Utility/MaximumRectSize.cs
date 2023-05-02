using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// From: https://forum.unity.com/threads/rect-transform-size-limiter.620860/#post-6285554

namespace BoundfoxStudios.FairyTaleDefender.UI.Utility
{
	[RequireComponent(typeof(RectTransform))]
	[ExecuteInEditMode]
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(MaximumRectSize))]
	public class MaximumRectSize : UIBehaviour, ILayoutSelfController
	{
		private RectTransform _rectTransform = default!;

		[SerializeField]
		// ReSharper disable once InconsistentNaming
		private Vector2 _maxSize = Vector2.zero;

		private DrivenRectTransformTracker _tracker;

		protected override void OnEnable()
		{
			_rectTransform = GetComponent<RectTransform>();

			base.OnEnable();
			SetDirty();
		}

		protected override void OnDisable()
		{
			_tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(_rectTransform);
			base.OnDisable();
		}

#if UNITY_EDITOR
		protected override void OnValidate()
		{
			base.OnValidate();
			SetDirty();
		}
#endif

		public void SetLayoutHorizontal()
		{
			if (_maxSize.x > 0f && _rectTransform.rect.width > _maxSize.x)
			{
				_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxSize.x);
				_tracker.Add(this, _rectTransform, DrivenTransformProperties.SizeDeltaX);
			}
		}

		public void SetLayoutVertical()
		{
			if (_maxSize.y > 0f && _rectTransform.rect.height > _maxSize.y)
			{
				_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _maxSize.y);
				_tracker.Add(this, _rectTransform, DrivenTransformProperties.SizeDeltaY);
			}
		}

		private void SetDirty()
		{
			if (!IsActive())
			{
				return;
			}

			LayoutRebuilder.MarkLayoutForRebuild(_rectTransform);
		}
	}
}
