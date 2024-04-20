using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace ByteBuoy.API.Extensions
{
	public static class HttpResponseExtensions
	{
		public static void AddPaginationHeader(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
		{
			var paginationHeader = new
			{
				currentPage,
				itemsPerPage,
				totalItems,
				totalPages
			};

			response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(paginationHeader));
		}
	}

}
