using System;
using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects.CallbackProcessors
{
	//We don't need more than one instance of this.
	[CreateAssetMenu(menuName = Constants.MenuNames.Input + "/Pause Actions")]
	public class PauseActionsSO : ScriptableObject, GameInput.IPauseActions
	{
		public event Action ResumeGame = delegate { };

		public void OnResumeGame(InputAction.CallbackContext context)
		{
			if (!context.canceled)
			{
				return;
			}

			ResumeGame.Invoke();
		}
	}
}
