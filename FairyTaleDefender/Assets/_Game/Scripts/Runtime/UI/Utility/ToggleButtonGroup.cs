using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace BoundfoxStudios.FairyTaleDefender.UI.Utility
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(ToggleButtonGroup))]
	public class ToggleButtonGroup : MonoBehaviour
	{
		[field: SerializeField]
		private int InitialActivatedIndex { get; set; }

		[field: SerializeField]
		private Color SelectedColorTint { get; set; } = new(0.67f, 0.67f, 0.67f);

		private struct ToggleButton
		{
			public Button Button { get; set; }
			public IToggleButtonAction? Action { get; set; }
			public Image Image { get; set; }
		}

		private ToggleButton[] _buttons = { };


		private void Awake()
		{
			var buttons = GetComponentsInChildren<Button>();
			_buttons = buttons.Select(button => new ToggleButton()
			{
				Button = button,
				Action = button.GetComponent<IToggleButtonAction>(),
				Image = button.GetComponent<Image>(),
			}).ToArray();

			VerifyActions(_buttons);
			AssignListeners(_buttons);
			UpdateSelectedButton(InitialActivatedIndex);
		}

		private void Start()
		{
			ButtonClicked(InitialActivatedIndex);
		}

		private void OnDestroy()
		{
			foreach (var toggleButton in _buttons)
			{
				toggleButton.Button.onClick.RemoveAllListeners();
			}
		}

		[Conditional("UNITY_EDITOR")]
		private void VerifyActions(IEnumerable<ToggleButton> buttons)
		{
			foreach (var button in buttons)
			{
				if (button.Action == null)
				{
					Debug.LogError("Missing IToggleButtonAction on ToggleButton", button.Button);
				}
			}
		}

		private void AssignListeners(IReadOnlyList<ToggleButton> buttons)
		{
			for (var i = 0; i < buttons.Count; i++)
			{
				var toggleButton = buttons[i];

				var index = i;
				toggleButton.Button.onClick.AddListener(() => ButtonClicked(index));
			}
		}

		private void ButtonClicked(int index)
		{
			UpdateSelectedButton(index);
			_buttons[index].Action?.ExecuteAction();
		}

		private void UpdateSelectedButton(int index)
		{
			for (var i = 0; i < _buttons.Length; i++)
			{
				var toggleButton = _buttons[i];
				toggleButton.Image.color = i == index ? SelectedColorTint : new(1, 1, 1);
			}
		}
	}

	public interface IToggleButtonAction
	{
		void ExecuteAction();
	}
}
