using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Audio.ScriptableObjects
{
	/// <summary>
	/// Container for <see cref="PlaylistItemSO"/>s
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.Audio + "/Playlist")]
	public class PlaylistSO : ScriptableObject
	{
		[SerializeField]
		private PlaylistItemSO[] Clips;

		private int _lastClipIndex;
		private int _nextClipIndex;

		public AudioClip GetNextRandomClipWithoutImmediateRepeat()
		{
			do
			{
				_nextClipIndex = Random.Range(0, Clips.Length);
			} while (_nextClipIndex == _lastClipIndex && Clips[_nextClipIndex].AudioClip != null);

			_lastClipIndex = _nextClipIndex;

			return Clips[_nextClipIndex].AudioClip;
		}
	}
}
