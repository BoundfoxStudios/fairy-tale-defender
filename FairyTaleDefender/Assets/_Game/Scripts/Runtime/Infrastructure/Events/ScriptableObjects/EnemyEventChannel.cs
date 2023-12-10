using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Characters.Enemies;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Enemy_EventChannel", menuName = Constants.MenuNames.Events + "/Enemy Event Channel")]
	public class EnemyEventChannelSO : EventChannelSO<Enemy>
	{
		// Marker Class
	}
}
