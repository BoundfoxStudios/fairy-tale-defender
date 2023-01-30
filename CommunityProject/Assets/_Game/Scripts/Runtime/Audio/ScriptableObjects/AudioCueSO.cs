using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Audio.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Audio + "/Audio Cue")]
	public class AudioCueSO : ScriptableObject
	{
		[field: SerializeField]
		public AudioClip AudioClip { get; private set; } = default!;
	}
}
