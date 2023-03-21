using BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Systems.InputSystem;
using BoundfoxStudios.CommunityProject.Systems.InputSystem.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoundfoxStudios.CommunityProject
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/DisplayToolTip Event Channel")]
	public class DisplayToolTipEventChannelSO : EventChannelSO<string>, GameInput.IToolTipsActions
	{
		public event InputReaderSO.ScreenPositionHandler Position = delegate { };
		public void OnToolTipPosition(InputAction.CallbackContext context)
		{
			if(!context.performed)
			{
				return;
			}
			Position(context.ReadValue<Vector2>());
		}
	}
}
