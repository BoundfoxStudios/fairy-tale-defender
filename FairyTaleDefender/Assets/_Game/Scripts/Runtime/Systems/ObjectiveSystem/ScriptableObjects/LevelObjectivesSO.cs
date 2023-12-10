using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.ObjectiveSystem.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Level_X_Objectives", menuName = Constants.MenuNames.Objectives + "/Level Objectives")]
	public class LevelObjectivesSO : ScriptableObject
	{
		[field: SerializeField]
		public ObjectiveSO[] Objectives { get; private set; } = default!;
	}
}
