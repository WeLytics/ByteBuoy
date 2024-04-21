using ByteBuoy.Application.DTO;

namespace ByteBuoy.API.Utilities
{
	public static class PaginationMetaBuilder
	{
		public static PaginationMeta Build(int currentPage, int pageSize, int totalItems, int totalPages)
		{
			return new PaginationMeta
			{
				CurrentPage = currentPage,
				PageSize = pageSize,
				TotalItems = totalItems,
				TotalPages = totalPages
			};
		}
	}
}
