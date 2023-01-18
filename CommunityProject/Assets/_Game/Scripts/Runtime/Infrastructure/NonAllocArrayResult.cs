namespace BoundfoxStudios.CommunityProject.Infrastructure
{
	public struct NonAllocArrayResult<T>
	{
		public int Size { get; set; }
		public T[] Result { get; set; }

		public T this[int index]
		{
			get => Result[index];
			set => Result[index] = value;
		}

		public static implicit operator int(NonAllocArrayResult<T> item) => item.Size;
	}
}
