using BoundfoxStudios.FairyTaleDefender.Entities.Characters.Enemies.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Enemy Event Channel")]
	public class EnemyEventChannelSO : EventChannelSO<EnemySO>
	{
		// Marker Class
	}
}
