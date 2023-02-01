using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoundfoxStudios.CommunityProject.Input
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public class EdgePanProcessor : InputProcessor<Vector2>
	{
		// ReSharper disable once FieldCanBeMadeReadOnly.Global
		// ReSharper disable once MemberCanBePrivate.Global
		// ReSharper disable once ConvertToConstant.Global
		// Must be public writeable to show up in the InputAction's Editor.
		public int EdgePanThreshold = 25;

#if UNITY_EDITOR
		static EdgePanProcessor()
		{
			Initialize();
		}
#endif

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			InputSystem.RegisterProcessor<EdgePanProcessor>();
		}

		public override Vector2 Process(Vector2 value, InputControl control)
		{
			var deltaMovement = Vector2.zero;

			if (value.x < EdgePanThreshold)
			{
				deltaMovement.x = -1;
			}
			else if (value.x > Screen.width - EdgePanThreshold)
			{
				deltaMovement.x = 1;
			}

			if (value.y < EdgePanThreshold)
			{
				deltaMovement.y = -1;
			}
			else if (value.y > Screen.height - EdgePanThreshold)
			{
				deltaMovement.y = 1;
			}

			return deltaMovement;
		}
	}
}
