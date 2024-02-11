using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Extensions;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure.SceneManagement.ScriptableObjects
{
	/// <summary>
	/// Collection of all level packs
	/// </summary>
	// We don't need more than one instance.
	// [CreateAssetMenu(menuName = Constants.MenuNames.SceneManagement + "/All Level Packs")]
	public class AllLevelPacksSO : ScriptableObject
	{
		[field: SerializeField]
		public LevelPackSO[] LevelPacks { get; set; } = default!;

		private void OnEnable()
		{
			LinkAllLevel();
		}

		public LevelSO? FindByIdentity(ScriptableObjectIdentity identity) => LevelPacks.SelectMany(packs => packs.Levels)
			.SingleOrDefault(level => level.Identity == identity);

		public void LinkAllLevel()
		{
			if (LevelPacks == null!)
			{
				return;
			}

			for (var i = 0; i < LevelPacks.Length; i++)
			{
				var levelPack = LevelPacks[i];

				if (!levelPack.Exists())
				{
					continue;
				}

				LinkLevelPackWithNextLevelPack(levelPack, i);

				for (var j = 0; j < levelPack.Levels.Length; j++)
				{
					var level = levelPack.Levels[j];

					if (!level.Exists())
					{
						continue;
					}

					LinkLevelWithNextLevel(levelPack, level, i, j);
				}
			}
		}

		private void LinkLevelWithNextLevel(LevelPackSO levelPack, LevelSO level, int levelPackIndex, int levelIndex)
		{
			var isLastLevelOfLevelPack = levelIndex == levelPack.Levels.Length - 1;

			if (isLastLevelOfLevelPack && levelPackIndex == LevelPacks.Length - 1)
			{
				//This is the last level of the last level pack.
				//At this point we have already connected it with the previous one, so we can return.
				return;
			}

			var nextLevel = isLastLevelOfLevelPack
				? levelPack.NextLevelPack!.Levels[0]
				: levelPack.Levels[levelIndex + 1];

			level.NextLevel = nextLevel;
			nextLevel.PreviousLevel = level;
		}

		private void LinkLevelPackWithNextLevelPack(LevelPackSO levelPack, int index)
		{
			if (index == LevelPacks.Length - 1)
			{
				return;
			}

			var nextLevelPack = LevelPacks[index + 1];

			levelPack.NextLevelPack = nextLevelPack;
			nextLevelPack.PreviousLevelPack = levelPack;
		}
	}
}
