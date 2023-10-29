using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BoundfoxStudios.FairyTaleDefender.EditorExtensions.ScriptableObjects
{
	/// <summary>
	///   This class manages the prefabs that the user can select in the editor.
	/// </summary>
	/// <remarks>
	///   This class must not be in an Editor assembly, otherwise the SO won't work.
	/// </remarks>
	// Removed [CreateAssetMenu], we only need one instance.
	// [CreateAssetMenu]
	public class PrefabManagerSO : ScriptableObject
	{
#if UNITY_EDITOR
		// Careful, please do not change to [field: SerializedField] in this class, it's just an Editor script
		public GameObject Canvas = default!;
		public GameObject WorldSpaceCanvas = default!;

		public TextPrefabs Texts = default!;
		public BarPrefabs Bars = default!;
		public ButtonPrefabs Buttons = default!;
		public ContainerPrefabs Containers = default!;
		public InputPrefabs Inputs = default!;
		public CameraPrefabs Cameras = default!;
		public LightPrefabs Lights = default!;
		public EditorPrefabs Editor = default!;

		[Serializable]
		public class TextPrefabs
		{
			public GameObject Text = default!;
			public GameObject SettingsText = default!;
		}

		[Serializable]
		public class BarPrefabs
		{
			public GameObject Bar = default!;
			public GameObject HealthBar = default!;
		}

		[Serializable]
		public class ButtonPrefabs
		{
			public GameObject BrownButton = default!;
		}

		[Serializable]
		public class InputPrefabs
		{
			public GameObject InputField = default!;
			public GameObject Toggle = default!;
			public GameObject Dropdown = default!;
		}

		[Serializable]
		public class ContainerPrefabs
		{
			public GameObject TabGroup = default!;
			public GameObject TabGroupHeaderButton = default!;
		}

		[Serializable]
		public class CameraPrefabs
		{
			public GameObject Menu = default!;
			public GameObject Level = default!;
		}

		[Serializable]
		public class LightPrefabs
		{
			public GameObject Sun = default!;
		}

		[Serializable]
		public class EditorPrefabs
		{
			public GameObject EditorColdStartup = default!;
		}
#endif
	}
}
