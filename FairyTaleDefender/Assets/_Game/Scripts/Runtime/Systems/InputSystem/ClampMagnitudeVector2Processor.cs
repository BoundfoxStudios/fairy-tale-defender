using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoundfoxStudios.CommunityProject.Systems.InputSystem
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public class ClampMagnitudeVector2Processor : InputProcessor<Vector2>
	{
		// ReSharper disable once FieldCanBeMadeReadOnly.Global
		// ReSharper disable once MemberCanBePrivate.Global
		// ReSharper disable once ConvertToConstant.Global
		// Must be public writeable to show up in the InputAction's Editor.
		public float MaxLength = 1;

#if UNITY_EDITOR
		static ClampMagnitudeVector2Processor()
		{
			Initialize();
		}
#endif

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			UnityEngine.InputSystem.InputSystem.RegisterProcessor<ClampMagnitudeVector2Processor>();
		}

		public override Vector2 Process(Vector2 value, InputControl control) => Vector2.ClampMagnitude(value, MaxLength);
	}
}
