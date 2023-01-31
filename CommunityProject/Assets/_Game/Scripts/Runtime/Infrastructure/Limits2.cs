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
	}
}
