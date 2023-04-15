using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.SpawnSystem.Waves.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.SpawnSystem + "/Waves")]
	public class WavesSO : ScriptableObject
	{
		[field: SerializeReference]
		public Wave[] Waves { get; set; } = default!;
	}
}
