using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace BoundfoxStudios.FairyTaleDefender.UI.Confirmation
{
	/// <summary>
	/// Use this component if there is more than one system in the scene which could use the same <see cref="ConfirmUI"/>
	/// and set callbacks dynamically.
	/// </summary>
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(OpenConfirmUI))]
	public class OpenConfirmUI : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private ConfirmUI ConfirmUI { get; set; } = default!;

		[field: Header("Settings")]
		[field: SerializeField]
		private LocalizedString ConfirmationMessage { get; set; } = default!;

		[field: SerializeField]
		private UnityEvent ConfirmAction { get; set; } = default!;

		[field: SerializeField]
		private UnityEvent CancelAction { get; set; } = default!;

		public void Open()
		{
			ConfirmUI.Open(ConfirmationMessage, ConfirmAction, CancelAction);
		}
	}
}
