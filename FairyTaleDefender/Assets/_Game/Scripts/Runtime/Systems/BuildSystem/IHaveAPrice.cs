namespace BoundfoxStudios.FairyTaleDefender.Systems.BuildSystem
{
	public interface IHaveAPrice
	{
		/// <summary>
		/// The price to build this thing.
		/// </summary>
		int Price { get; }
	}
}
