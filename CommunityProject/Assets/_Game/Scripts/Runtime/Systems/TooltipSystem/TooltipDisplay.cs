using System;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Systems.TooltipSystem
{
	public abstract class TooltipDisplay : MonoBehaviour
	{
		[field: SerializeField]
		protected RectTransform ContainerRectTransform { get; private set; } = default!;

		protected abstract void SetTooltip<T>(T tooltip) where T : ITooltip2;

		protected TResult ResolveTooltip<TResult, T>(T tooltip)
			where TResult : ITooltip2
			where T : ITooltip2
		{
			if (tooltip is TResult result)
			{
				return result;
			}

			throw new($"{typeof(TResult).Name} expected, but got {typeof(T).Name}");
		}

		public void Show<T>(T tooltip, Vector2 screenPosition) where T : ITooltip2
		{
			SetTooltip(tooltip);
			SetPosition(screenPosition);

			ContainerRectTransform.gameObject.SetActive(true);
		}

		public void Hide()
		{
			ContainerRectTransform.gameObject.SetActive(false);
		}

		public void SetPosition(Vector2 screenPosition)
		{
			var size = ContainerRectTransform.sizeDelta;
			var halfSizeX = size.x / 2;
			var offsetPosition = screenPosition + new Vector2(-halfSizeX, 5);

			offsetPosition = ContainPositionInScreenBoundary(offsetPosition, size);

			ContainerRectTransform.anchoredPosition = offsetPosition;
		}

		private Vector2 ContainPositionInScreenBoundary(Vector2 offsetPosition, Vector2 size)
		{
			if (offsetPosition.x < 0)
			{
				offsetPosition.x = 0;
			}
			else if (offsetPosition.x + size.x > Screen.width)
			{
				offsetPosition.x = Screen.width - size.x;
			}

			if (offsetPosition.y < 0)
			{
				offsetPosition.y = 0;
			}
			else if (offsetPosition.y + size.y > Screen.height)
			{
				offsetPosition.y = Screen.height - size.y;
			}

			return offsetPosition;
		}
	}
}
