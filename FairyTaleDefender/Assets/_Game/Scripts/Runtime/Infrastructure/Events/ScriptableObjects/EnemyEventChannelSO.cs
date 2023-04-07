using BoundfoxStudios.CommunityProject.Entities.Characters.Enemies.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Infrastructure.Events.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.Events + "/Enemy Event Channel")]
	public class EnemyEventChannelSO : EventChannelSO<EnemySO>
	{
		// Marker Class
	}
}
