using UnityEngine;
using Random = UnityEngine.Random;

namespace BoundfoxStudios.CommunityProject.Audio.ScriptableObjects
{
	/// <summary>
	/// Container for <see cref="PlaylistItemSO"/>s.
	/// </summary>
	[CreateAssetMenu(menuName = Constants.MenuNames.Audio + "/Playlist")]
	public class PlaylistSO : ScriptableObject
	{
		[SerializeField]
		private PlaylistItemSO[] Clips = default!;

		private int _lastClipIndex;
		private int _nextClipIndex;

		private void OnValidate()
		{
			Debug.Assert(Clips is not null || Clips!.Length > 0, "No PlaylistItems", this);
		}

		public AudioClip GetNextRandomClipWithoutImmediateRepeat()
		{
			if (Clips.Length == 1)
			{
				return Clips[0].AudioClip;
			}

			do
			{
				_nextClipIndex = Random.Range(0, Clips.Length);
			} while (_nextClipIndex == _lastClipIndex);

			_lastClipIndex = _nextClipIndex;

			return Clips[_nextClipIndex].AudioClip;
		}
	}
}
