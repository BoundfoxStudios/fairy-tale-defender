using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


namespace BoundfoxStudios.CommunityProject.UI
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class TMPLinks : MonoBehaviour, IPointerClickHandler {
		private TextMeshProUGUI _pTextMeshPro;
		private Canvas _pCanvas;
		private Camera pCamera;

		private void Awake() {
			_pTextMeshPro = GetComponent<TextMeshProUGUI>();
			_pCanvas = GetComponentInParent<Canvas>();

			// Get a reference to the camera if Canvas Render Mode is not ScreenSpace Overlay.
			if( _pCanvas.renderMode == RenderMode.ScreenSpaceOverlay )
				pCamera = null;
			else
				pCamera = _pCanvas.worldCamera;
		}

		public void OnPointerClick(PointerEventData eventData) {
			int linkIndex = TMP_TextUtilities.FindIntersectingLink(_pTextMeshPro, UnityEngine.Input.mousePosition, pCamera);
			if( linkIndex != -1 ) { // was a link clicked?
				TMP_LinkInfo linkInfo = _pTextMeshPro.textInfo.linkInfo[linkIndex];

				// open the link id as a url, which is the metadata we added in the text field
				Application.OpenURL(linkInfo.GetLinkID());
			}
		}

	}
}
