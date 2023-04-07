namespace BoundfoxStudios.CommunityProject.Infrastructure
{
	public struct NoAllocArrayResult<T>
	{
		public int Size { get; set; }
		public T[] Result { get; set; }

		public T this[int index]
		{
			get => Result[index];
			set => Result[index] = value;
		}

		public static implicit operator int(NoAllocArrayResult<T> item) => item.Size;
	}
}
