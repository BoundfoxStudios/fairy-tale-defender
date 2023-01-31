using BoundfoxStudios.CommunityProject.Infrastructure;
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
		[field: SerializeField]
		private PlaylistItemSO[] Clips { get; set; } = default!;

		private int _lastClipIndex;
		private int _nextClipIndex;

		private void OnValidate()
		{
			Guard.AgainstNull(() => Clips, this);
			Debug.Assert(Clips.Length > 0, "No PlaylistItems", this);
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
