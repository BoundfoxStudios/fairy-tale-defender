using BoundfoxStudios.FairyTaleDefender.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace BoundfoxStudios.FairyTaleDefender.UI.Confirmation
{
	/// <summary>
	/// This component is used to open a confirmation popup where the player has the chance to cancel the action,
	/// e.g. when return to main menu button is pressed while playing a level.
	/// Can act as a modal window given a <see cref="Panel"/> which covers the whole screen.
	/// </summary>
	/// <remarks>
	///	Actions could either be set directly on this component as long as there is only one system using this component,
	/// or from another component in case all could use the same layout with a different text.
	/// </remarks>
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(ConfirmUI))]
	public class ConfirmUI : MonoBehaviour
	{
		[field: Header("References")]
		[field: SerializeField]
		private GameObject Panel { get; set; } = default!;

		[field: SerializeField]
		private Button ConfirmButton { get; set; } = default!;

		[field: SerializeField]
		private Button CancelButton { get; set; } = default!;

		[field: SerializeField]
		private TextMeshProUGUI ConfirmationText { get; set; } = default!;

		[field: Header("Settings")]
		[field: SerializeField]
		private LocalizedString? ConfirmationMessage { get; set; }

		[field: SerializeField]
		private UnityEvent? ConfirmAction { get; set; }

		[field: SerializeField]
		private UnityEvent? CancelAction { get; set; }

		public void Open()
		{
			ConfirmationText.text = ConfirmationMessage!.GetLocalizedString();
			ConfirmButton.onClick.AddListener(ConfirmButtonPressed);
			CancelButton.onClick.AddListener(CancelButtonPressed);

			Panel.SetActive(true);
		}

		public void Open(LocalizedString confirmationMessage, UnityEvent confirmAction, UnityEvent cancelAction)
		{
			ConfirmationMessage = confirmationMessage;
			ConfirmAction = confirmAction;
			CancelAction = cancelAction;

			Open();
		}

		private void OnDisable()
		{
			RemoveListeners();
		}

		private void RemoveListeners()
		{
			ConfirmButton.onClick.RemoveAllListeners();
			CancelButton.onClick.RemoveAllListeners();
		}

		private void ConfirmButtonPressed()
		{
			ConfirmAction?.Invoke();
			ClosePanel();
		}

		private void CancelButtonPressed()
		{
			CancelAction?.Invoke();
			ClosePanel();
		}

		private void ClosePanel()
		{
			RemoveListeners();
			Panel.SetActive(false);
		}
	}
}
