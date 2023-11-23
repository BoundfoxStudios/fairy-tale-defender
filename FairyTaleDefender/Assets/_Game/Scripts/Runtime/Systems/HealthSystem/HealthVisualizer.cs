using BoundfoxStudios.FairyTaleDefender.Common;
using Unity.Mathematics;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.HealthSystem
{
	[AddComponentMenu(Constants.MenuNames.HealthSystem + "/" + nameof(HealthVisualizer))]
	public class HealthVisualizer : MonoBehaviour
	{
		[field: SerializeField]
		[field: Tooltip("Objects must be sorted from high to low")]
		private GameObject[] Visuals { get; set; } = default!;

		private void Start()
		{
			ActivateIndex(0);
		}

		public void UpdateVisuals(int current, int maximum)
		{
			var index = Mathf.FloorToInt(math.remap(maximum, 1, 0, Visuals.Length - 1, current));

			ActivateIndex(index);
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
