using BoundfoxStudios.FairyTaleDefender.Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

namespace BoundfoxStudios.FairyTaleDefender.UI.Utility
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(TMPLinks))]
	public class TMPLinks : MonoBehaviour, IPointerClickHandler
	{
		private Camera? _camera;
		private Canvas _canvas = default!;
		private TextMeshProUGUI _textMeshPro = default!;

		private void Awake()
		{
			_textMeshPro = GetComponent<TextMeshProUGUI>();
			_canvas = GetComponentInParent<Canvas>();

			// Get a reference to the camera if Canvas Render Mode is not ScreenSpace Overlay.
			_camera = _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			var linkIndex = TMP_TextUtilities.FindIntersectingLink(_textMeshPro, eventData.position, _camera);

			if (linkIndex == -1)
			{
				return;
			}

			// was a link clicked?
			var linkInfo = _textMeshPro.textInfo.linkInfo[linkIndex];

			// open the link id as a url, which is the metadata we added in the text field
			Application.OpenURL(linkInfo.GetLinkID());
		}
	}
}
