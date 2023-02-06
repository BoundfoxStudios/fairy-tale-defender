using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Systems.AudioSystem.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Audio + "/Audio Cue")]
	public class AudioCueSO : ScriptableObject
	{
		[field: SerializeField]
		public AudioClip AudioClip { get; private set; } = default!;
	}
}
