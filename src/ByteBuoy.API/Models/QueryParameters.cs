namespace ByteBuoy.API.Models
{
	public class QueryParameters
	{
		//Sorting params
		public string OrderBy { get; set; } = string.Empty;
		public bool OrderAsc { get; set; } = true;

		//Pagination params
		const int _maxPageSize = 50;
		private int _pageSize = 10;
		public int PageNumber { get; set; } = 1;
		public int PageSize
		{
			get { return _pageSize; }
			set { _pageSize = (value > _maxPageSize) ? _maxPageSize : value; }
		}
	}
}
