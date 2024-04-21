using ByteBuoy.Application.DTO;

namespace ByteBuoy.API.Utilities
{
	public class PagedList<T> 
	{
		public PaginationMeta Pagination { get; set; } = new();

		public List<T> Data { get; set; } = [];

		public PagedList(List<T> items, int count, int pageNumber, int pageSize)
		{
			Pagination.TotalItems = count;
			Pagination.PageSize = pageSize;
			Pagination.CurrentPage = pageNumber;
			Pagination.TotalPages = (int)Math.Ceiling(count / (double)pageSize);

			Data.AddRange(items);
		}

		public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
		{
			var count = source.Count();
			var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

			return new PagedList<T>(items, count, pageNumber, pageSize);
		}
	}
}
