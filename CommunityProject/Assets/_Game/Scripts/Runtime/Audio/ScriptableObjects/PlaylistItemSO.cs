using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Audio.ScriptableObjects
{
	/// <summary>
	/// <see cref="ScriptableObject"/> for holding a reference to an <see cref="AudioClip"/> and some Information about it.
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.Audio + "/Playlist Item")]
	public class PlaylistItemSO : ScriptableObject
	{
		public AudioClip AudioClip;
		public string Title;
		public string Interpreter;
		public string Url;

		private void OnValidate()
		{
			if (AudioClip == null)
			{
				Debug.LogWarning("No AudioClip assigned", this);
			}
		}
	}
}
