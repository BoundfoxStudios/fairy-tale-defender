using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.InputSystem
{
	/// <summary>
	/// Simple script that make a UI object follow the mouse.
	/// </summary>
	[AddComponentMenu(Constants.MenuNames.Input + "/" + nameof(UIFollowMouse))]
	public class UIFollowMouse : MonoBehaviour
	{
		[field: SerializeField]
		private InputReaderSO InputReader { get; set; } = default!;

		private Vector2 _desiredPosition = Vector2.zero;
		private RectTransform _rectTransform = default!;

		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
		}

		private void OnEnable()
		{
			InputReader.UIEffectsActions.Position += SetPosition;
		}

		private void OnDisable()
		{
			InputReader.UIEffectsActions.Position -= SetPosition;
		}

		private void SetPosition(Vector2 screenPosition)
		{
			_desiredPosition = screenPosition;
		}

		private void Update()
		{
			_rectTransform.anchoredPosition = _desiredPosition;
		}
	}
}
