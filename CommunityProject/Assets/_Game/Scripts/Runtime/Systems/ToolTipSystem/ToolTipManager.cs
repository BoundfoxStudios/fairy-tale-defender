using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using BoundfoxStudios.CommunityProject.Systems.InputSystem.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Systems.InputSystem.ScriptableObjects.CallbackProcessors;
using UnityEngine.Windows;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.InputSystem.Interactions;
using System.Runtime.CompilerServices;

namespace BoundfoxStudios.CommunityProject
{
	public class ToolTipManager : MonoBehaviour
	{
		//public static ToolTipManager? Instance;

		[field: SerializeField]
		private TMP_Text ToolTipText { get; set; } = default!;

		[field: SerializeField]
		public DisplayToolTipEventChannelSO DisplayToolTipEventChannel { get; private set; } = default!;

		[field: SerializeField]
		public VoidEventChannelSO DisableToolTipEventChannel { get; private set; } = default!;

		List<ToolTip> textToolTips = new List<ToolTip>();
		float toolTipOffsetX;
		float toolTipOffsetY;



		private void OnEnable()
		{
			toolTipOffsetX = ToolTipText.rectTransform.sizeDelta.x;
			toolTipOffsetY = ToolTipText.rectTransform.sizeDelta.y;

			textToolTips = FindObjectsByType<ToolTip>(FindObjectsSortMode.None).ToList();


			DisplayToolTipEventChannel!.Raised += DisplayToolTip;
			DisableToolTipEventChannel!.Raised += DisableToolTip;

			DisplayToolTipEventChannel!.Position += SetToolTipTextPosition;
		}
		private void OnDisable()
		{
			DisplayToolTipEventChannel!.Raised -= DisplayToolTip;
			DisableToolTipEventChannel!.Raised -= DisableToolTip;

			DisplayToolTipEventChannel!.Position -= SetToolTipTextPosition;
		}
		private void DisplayToolTip(string tip)
		{
			foreach (var textTip in textToolTips)
			{
				if (textTip.ToolTipText == tip)
				{
					ToolTipText.text = tip;

					ToolTipText.gameObject.SetActive(true);
				}
			}
		}

		private void SetToolTipTextPosition(Vector2 pos)
		{
			var mousepos = pos;
			var divider = 2.4f;

			var midScreenX = Screen.width / 2;
			var midScreenY = Screen.height / 2;

			float offsetX = toolTipOffsetX / divider;
			float offsetY = toolTipOffsetY;
			if (mousepos.y > midScreenY)
			{
				offsetY = -offsetY;
			}
			if (mousepos.x > midScreenX)
			{
				offsetX = -offsetX;
			}
			var newToolTipPosition = new Vector3(mousepos.x + offsetX, mousepos.y + offsetY, ToolTipText.transform.position.z);
			ToolTipText.transform.position = newToolTipPosition;


		}

		private void DisableToolTip()
		{
			if (ToolTipText.gameObject.activeSelf)
				ToolTipText.gameObject.SetActive(false);
		}

	}
}
