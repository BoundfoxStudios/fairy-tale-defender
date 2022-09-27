using System;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.EditorExtensions.ScriptableObjects
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
		public GameObject Canvas;

		public TextPrefabs Texts;
		public CameraPrefabs Cameras;
		public EditorPrefabs Editor;

		[Serializable]
		public class TextPrefabs
		{
			public GameObject Text;
		}

		[Serializable]
		public class CameraPrefabs
		{
			public GameObject Menu;
			public GameObject Level;
		}

		[Serializable]
		public class EditorPrefabs
		{
			public GameObject EditorColdStartup;
		}
#endif
	}
}
