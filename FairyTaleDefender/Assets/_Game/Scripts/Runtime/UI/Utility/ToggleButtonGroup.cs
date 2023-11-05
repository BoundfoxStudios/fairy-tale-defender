using System;
using System.Collections.Generic;
using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.FairyTaleDefender.UI.Utility
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(ToggleButtonGroup))]
	public class ToggleButtonGroup : MonoBehaviour
	{
		[field: SerializeField]
		private int InitialActivatedIndex { get; set; }

		[field: SerializeField]
		private bool ActivateInitialIndexOnStart { get; set; } = default!;

		[field: SerializeField]
		private Color SelectedColorTint { get; set; } = new(0.67f, 0.67f, 0.67f);

		public event Action<int> IndexChanged = delegate { };

		private int _index;

		public int Index
		{
			get => _index;
			set
			{
				_index = value;
				UpdateSelectedButton(_index);
			}
		}

		private struct ToggleButton
		{
			public Button Button { get; set; }
			public Image Image { get; set; }
		}

		private ToggleButton[] _buttons = { };


		private void Awake()
		{
			var buttons = GetComponentsInChildren<Button>();
			_buttons = buttons.Select(button => new ToggleButton()
			{
				Button = button,
				Image = button.GetComponent<Image>(),
			}).ToArray();

			AssignListeners(_buttons);
			UpdateSelectedButton(InitialActivatedIndex);
		}

		private void Start()
		{
			if (ActivateInitialIndexOnStart)
			{
				ButtonClicked(InitialActivatedIndex);
			}
		}

		private void OnDestroy()
		{
			foreach (var toggleButton in _buttons)
			{
				toggleButton.Button.onClick.RemoveAllListeners();
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
			Index = index;
			IndexChanged(index);
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
}
