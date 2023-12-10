using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Entities.Characters.Enemies;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.RuntimeSets
{
	[CreateAssetMenu(menuName = Constants.MenuNames.RuntimeSets + "/" + nameof(EnemyRuntimeSetSO))]
	public class EnemyRuntimeSetSO : RuntimeSetSO<Enemy>
	{
	}
}
