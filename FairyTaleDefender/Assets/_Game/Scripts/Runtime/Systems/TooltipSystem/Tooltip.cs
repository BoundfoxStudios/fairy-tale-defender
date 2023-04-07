using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BoundfoxStudios.FairyTaleDefender.Systems.TooltipSystem
{
	public abstract class Tooltip : MonoBehaviour, ITooltip, IPointerEnterHandler, IPointerExitHandler
	{
		[field: SerializeField]
		public TooltipEventChannelSO ShowTooltipEventChannel { get; private set; } = default!;

		[field: SerializeField]
		public VoidEventChannelSO HideTooltipEventChannel { get; private set; } = default!;

		public void OnPointerEnter(PointerEventData eventData)
		{
			ShowTooltipEventChannel.Raise(new()
			{
				Tooltip = this,
				ScreenPosition = eventData.position
			});
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			HideTooltipEventChannel.Raise();
		}
	}
}
