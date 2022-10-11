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

		private bool _isValid;

		private void OnValidate()
		{
			foreach (var item in Clips)
			{
				if (item.AudioClip == null)
				{
					_isValid = false;
					return;
				}
			}

			_isValid = true;
		}

		public AudioClip GetNextRandomClipWithoutImmediateRepeat()
		{
			if (_isValid == false)
			{
				Debug.LogError("Missing AudioClip(s) in playlist", this);
				return null;
			}

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
