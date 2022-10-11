using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Audio
{
	/// <summary>
	/// <see cref="ScriptableObject"/> for holding a reference to an <see cref="AudioClip"/> and some Information about it.
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.Audio + "/Playlist Item")]
    public class PlaylistItemSO : ScriptableObject
    {
		[SerializeField]
		private AudioClip audioClip;

		[SerializeField]
		private string title;

		[SerializeField]
		private string interpreter;

		[SerializeField]
		private string url;

		public AudioClip AudioClip => audioClip;
		public string Title => title;
		public string Interpreter => interpreter;
		public string Url => url;
    }
}
