namespace ByteBuoy.Application.DTO
{
	internal class PagedResult<T>
	{
		public IEnumerable<T> Data { get; set; } = null!;
		public PaginationMeta Pagination { get; set; } = null!;
	}
}
