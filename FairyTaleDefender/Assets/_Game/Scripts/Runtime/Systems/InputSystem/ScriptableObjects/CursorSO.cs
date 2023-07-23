using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Input + "/Cursor")]
	public class CursorSO : ScriptableObject
	{
		[field: SerializeField]
		public Texture2D[] Textures { get; private set; } = default!;

		[field: SerializeField]
		public float AnimationDelay { get; private set; } = 0.2f;

		[field: SerializeField]
		public Vector2 Hotspot { get; private set; } = default!;
	}
}
