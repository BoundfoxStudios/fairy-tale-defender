using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Characters.Enemies.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(fileName = Constants.FileNames.EventChannelSuffix,
		menuName = Constants.MenuNames.Events + "/EnemySO" + Constants.MenuNames.EventChannelSuffix)]
	public class EnemySOEventChannelSO : EventChannelSO<EnemySO>
	{
		// Marker Class
	}
}
