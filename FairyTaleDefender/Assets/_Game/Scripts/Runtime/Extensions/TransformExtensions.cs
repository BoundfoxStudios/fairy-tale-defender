using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Extensions
{
	public static class TransformExtensions
	{
		public static void DOQuarterRotationAroundY(this Transform transform, float timeToRotate, CancellationToken destroyCancellationToken)
		{
			transform.DOComplete();
			transform.DOLocalRotate(transform.rotation.eulerAngles + new Vector3(0, 90, 0),
					timeToRotate,
					RotateMode.FastBeyond360)
				.AwaitWithCancellation(destroyCancellationToken)
				.Forget();
		}

		public static void ClearChildren(this Transform transform)
		{
			for (var i = transform.childCount - 1; i >= 0; i--)
			{
				var child = transform.GetChild(i);
				Object.Destroy(child.gameObject);
			}
		}
	}
}
