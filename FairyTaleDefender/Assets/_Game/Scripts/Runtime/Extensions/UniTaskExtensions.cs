using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace BoundfoxStudios.FairyTaleDefender.Extensions
{
	public static class UniTaskExtensions
	{
		public static async UniTask AwaitWithCancellation(this Tween tween, CancellationToken cancellationToken)
		{
			if (!tween.IsActive())
			{
				await UniTask.CompletedTask;
			}

			await using var registration = cancellationToken.Register(() =>
			{
				if (tween.IsActive())
				{
					tween.Kill();
				}
			});

			await tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken);
		}
	}
}
