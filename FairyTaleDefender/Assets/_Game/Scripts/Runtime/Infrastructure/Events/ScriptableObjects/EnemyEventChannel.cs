using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Characters.Enemies;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(fileName = Constants.FileNames.EventChannelSuffix,
		menuName = Constants.MenuNames.Events + "/Enemy" + Constants.MenuNames.EventChannelSuffix)]
	public class EnemyEventChannelSO : EventChannelSO<Enemy>
	{
		// Marker Class
	}
}
