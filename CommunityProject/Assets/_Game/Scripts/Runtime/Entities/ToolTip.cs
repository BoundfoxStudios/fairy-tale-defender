using BoundfoxStudios.CommunityProject.Systems.InputSystem.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BoundfoxStudios.CommunityProject
{
    public class ToolTip : MonoBehaviour, ITextToolTip, IPointerEnterHandler, IPointerExitHandler
    {
		[field: SerializeField]
		string? toolTip = default!;
		public Vector3 Position => throw new System.NotImplementedException();

		public string ToolTipText { get { return toolTip!; } }

		public void OnPointerEnter(PointerEventData eventData)
		{
			ToolTipManager.Instance.DisplayToolTipEventChannel!.Raise(ToolTipText);
			Debug.Log("MouseEnter");
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			ToolTipManager.Instance.DisableToolTipEventChannel!.Raise();
			Debug.Log("MouseExit");
		}
	}
}
