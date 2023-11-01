using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.FairyTaleDefender.UI.Utility
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(MultiGraphicsTarget))]
	public class MultiGraphicsTarget : Graphic
	{
		[field: SerializeField]
		public Graphic[] Targets { get; private set; } = default!;

		public override void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
			foreach (var target in Targets)
			{
				target.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
			}
		}

		public override void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha,
			bool useRGB)
		{
			foreach (var target in Targets)
			{
				target.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha, useRGB);
			}
		}
	}
}
