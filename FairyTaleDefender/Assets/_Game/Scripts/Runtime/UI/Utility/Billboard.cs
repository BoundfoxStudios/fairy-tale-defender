using BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeAnchors.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI.Utility
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(Billboard))]
	public class Billboard : MonoBehaviour
	{
		[field: SerializeField]
		private CameraRuntimeAnchorSO CameraRuntimeAnchor { get; set; } = default!;

		private void LateUpdate()
		{
			if (CameraRuntimeAnchor.TryGetItem(out var mainCamera))
			{
				transform.LookAt(transform.position + mainCamera.transform.forward);
			}
		}
	}
}

