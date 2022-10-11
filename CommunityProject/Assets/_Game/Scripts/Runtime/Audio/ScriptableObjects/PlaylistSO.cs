using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Audio
{
	/// <summary>
	/// Container for <see cref="PlaylistItemSO"/>s
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.Audio + "/Playlist")]
	public class PlaylistSO : ScriptableObject
	{
		[SerializeField]
		private PlaylistItemSO[] clips;

		private int _lastClipIndex;
		private int _nextClipIndex;

		/// <summary>
		/// Returns an <see cref="AudioClip"/> in this playlist randomly
		/// </summary>
		/// <returns>Another <see cref="AudioClip"/> than the one previously played in this playlist</returns>
		public AudioClip GetNextRandomClipWithoutImmediateRepeat()
		{
			do
			{
				_nextClipIndex = Random.Range(0, clips.Length);
			} while (_nextClipIndex == _lastClipIndex);

			_lastClipIndex = _nextClipIndex;

			return clips[_nextClipIndex].AudioClip;
		}
	}
}
