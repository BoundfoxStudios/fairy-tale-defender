using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Extensions
{
	public static class TransformExtensions
	{
		public static void DOQuarterRotationAroundY(this Transform transform, float timeToRotate, CancellationToken destroyCancellationToken)
		{
			transform.DOComplete();
			transform.DOLocalRotate(transform.rotation.eulerAngles + new Vector3(0, 90, 0),
					timeToRotate,
					RotateMode.FastBeyond360)
				.WithCancellation(destroyCancellationToken);
		}
	}
}
