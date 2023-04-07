using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BoundfoxStudios.CommunityProject.Systems.TooltipSystem
{
	public abstract class Tooltip2 : MonoBehaviour, ITooltip, IPointerEnterHandler, IPointerExitHandler
	{
		[field: SerializeField]
		public TooltipEventChannelSO ShowTooltipEventChannel { get; private set; } = default!;

		[field: SerializeField]
		public VoidEventChannelSO HideTooltipEventChannel { get; private set; } = default!;

		protected abstract ITooltip GetTooltip();

		public void OnPointerEnter(PointerEventData eventData)
		{
			ShowTooltipEventChannel.Raise(new()
			{
				Tooltip = GetTooltip(),
				ScreenPosition = eventData.position
			});
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			HideTooltipEventChannel.Raise();
		}
	}
}
