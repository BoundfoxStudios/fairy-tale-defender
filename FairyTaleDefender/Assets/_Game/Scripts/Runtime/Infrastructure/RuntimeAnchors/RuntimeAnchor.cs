using BoundfoxStudios.CommunityProject.Infrastructure.RuntimeAnchors.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Infrastructure.RuntimeAnchors
{
	[DefaultExecutionOrder(-1000)]
	public class RuntimeAnchor : MonoBehaviour
	{
		[field: SerializeField]
		private RuntimeAnchorBaseSO Anchor { get; set; } = default!;

		private void Awake()
		{
			if (!gameObject.TryGetComponent(Anchor.Type, out var component))
			{
				Debug.LogError($"Missing {Anchor.Type.Name} on this object", gameObject);
				return;
			}

			Anchor.ManagedObject = component;
		}

		private void OnDestroy()
		{
			Anchor.ManagedObject = null;
		}
	}
}
