using BoundfoxStudios.CommunityProject.Systems.NavigationSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Splines;

namespace BoundfoxStudios.CommunityProject.Systems.SpawnSystem
{
	// TODO: This will be replaced later and is just a test script.
	public class Spawner : MonoBehaviour
	{
		public SplineWalker ItemToSpawn = default!;
		public SplineContainer SplineContainer = default!;
		public int ToSpawn = 5;
		public int Delay = 1000;

		private void Awake()
		{
			SpawnAsync().Forget();
		}

		private async UniTaskVoid SpawnAsync()
		{
			for (var i = 0; i < ToSpawn; i++)
			{
				await UniTask.Delay(Delay);
				ItemToSpawn.Container = SplineContainer;
				Instantiate(ItemToSpawn);
			}
		}
	}
}