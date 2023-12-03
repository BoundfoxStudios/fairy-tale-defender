using BoundfoxStudios.FairyTaleDefender.Common;
using Unity.Mathematics;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.HealthSystem
{
	[AddComponentMenu(Constants.MenuNames.HealthSystem + "/" + nameof(HealthVisualizer))]
	public class HealthVisualizer : MonoBehaviour
	{
		private static readonly int Death = Animator.StringToHash("Death");

		[field: SerializeField]
		[field: Tooltip("Objects must be sorted from high to low")]
		private GameObject[] Visuals { get; set; } = default!;

		[field: SerializeField]
		private Animator Animator { get; set; } = default!;

		private void Start()
		{
			ActivateIndex(0);
		}

		public void UpdateVisuals(int current, int maximum)
		{
			var index = Mathf.FloorToInt(math.remap(maximum, 2, 0, Visuals.Length - 1, current));

			ActivateIndex(index);

			if (current == 1)
			{
				Animator.Play(Death);
			}
		}

		private void ActivateIndex(int index)
		{
			for (var i = 0; i < Visuals.Length; i++)
			{
				Visuals[i].SetActive(i == index);
			}
		}
	}
}
