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

		private void OnEnable()
		{
			if(Camera.main.gameObject.GetComponent<PhysicsRaycaster>() == null)
			{
				Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
			}
		}
		private void OnDisable()
		{
			if(Camera.main.gameObject.GetComponent<PhysicsRaycaster>() != null)
			{
				Destroy(Camera.main.gameObject.GetComponent<PhysicsRaycaster>());
				
			}
		}
		public void OnPointerEnter(PointerEventData eventData)
		{
			ToolTipManager.Instance.DisplayToolTipEventChannel!.Raise(ToolTipText);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			ToolTipManager.Instance.DisableToolTipEventChannel!.Raise();
		}
	}
}
