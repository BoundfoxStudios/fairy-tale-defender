using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.FairyTaleDefender.Systems.TooltipSystem
{
	public abstract class TooltipDisplay : MonoBehaviour
	{
		[field: SerializeField]
		protected RectTransform ContainerRectTransform { get; private set; } = default!;

		[field: SerializeField]
		private Canvas TooltipCanvas { get; set; } = default!;

		protected abstract void SetTooltip<T>(T tooltip) where T : ITooltip;

		protected TResult ResolveTooltip<TResult, T>(T tooltip)
			where TResult : ITooltip
			where T : ITooltip
		{
			if (tooltip is TResult result)
			{
				return result;
			}

			throw new($"{typeof(TResult).Name} expected, but got {typeof(T).Name}");
		}

		public void Show<T>(T tooltip, Vector2 screenPosition) where T : ITooltip
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
			var normalizedScreenPosition = screenPosition / TooltipCanvas.scaleFactor;
			var size = ContainerRectTransform.sizeDelta;
			var halfSizeX = size.x / 2;
			var offsetPosition = normalizedScreenPosition + new Vector2(-halfSizeX, 5);

			offsetPosition = ContainPositionInScreenBoundary(offsetPosition, size);

			ContainerRectTransform.anchoredPosition = offsetPosition;
		}

		private Vector2 ContainPositionInScreenBoundary(Vector2 offsetPosition, Vector2 size)
		{
			var scaleFactor = TooltipCanvas.scaleFactor;
			var normalizedScreenWidth = Screen.width / scaleFactor;
			var normalizedScreenHeight = Screen.height / scaleFactor;

			if (offsetPosition.x < 0)
			{
				offsetPosition.x = 0;
			}
			else if (offsetPosition.x + size.x > normalizedScreenWidth)
			{
				offsetPosition.x = normalizedScreenWidth - size.x;
			}

			if (offsetPosition.y < 0)
			{
				offsetPosition.y = 0;
			}
			else if (offsetPosition.y + size.y > normalizedScreenHeight)
			{
				offsetPosition.y = normalizedScreenHeight - size.y;
			}

			return offsetPosition;
		}
	}
}
