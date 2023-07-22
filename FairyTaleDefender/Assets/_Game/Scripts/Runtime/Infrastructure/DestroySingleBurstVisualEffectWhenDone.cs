using BoundfoxStudios.FairyTaleDefender.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

namespace BoundfoxStudios.FairyTaleDefender.Infrastructure
{
	[RequireComponent(typeof(VisualEffect))]
	[AddComponentMenu(Constants.MenuNames.Infrastructure + "/" + nameof(DestroySingleBurstVisualEffectWhenDone))]
	public class DestroySingleBurstVisualEffectWhenDone : MonoBehaviour
	{
		private void Awake()
		{
			DestroyAfterFinishAsync(GetComponent<VisualEffect>()).Forget();
		}

		private async UniTaskVoid DestroyAfterFinishAsync(VisualEffect visualEffect)
		{
			await UniTask.WaitWhile(() => visualEffect.aliveParticleCount == 0);
			await UniTask.WaitUntil(() => visualEffect.aliveParticleCount == 0);
			Destroy(gameObject);
		}
	}
}
