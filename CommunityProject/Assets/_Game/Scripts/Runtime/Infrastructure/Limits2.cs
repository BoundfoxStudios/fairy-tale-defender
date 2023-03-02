using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Infrastructure
{
	public struct Limits2
	{
		public float Minimum { get; set; }
		public float Maximum { get; set; }

		public Limits2(float minimum, float maximum)
		{
			Minimum = minimum;
			Maximum = maximum;
		}

		public void Deconstruct(out float minimum, out float maximum)
		{
			minimum = Minimum;
			maximum = Maximum;
		}

		public static implicit operator Vector2(Limits2 limits) => new(limits.Minimum, limits.Maximum);
		public static implicit operator Vector3(Limits2 limits) => new(limits.Minimum, limits.Maximum);
		public static implicit operator Vector4(Limits2 limits) => new(limits.Minimum, limits.Maximum);
	}
}
