using System.Linq.Expressions;

namespace ByteBuoy.API.Extensions
{
	public static class QueryableExtensions
	{
		public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy, Dictionary<string, Expression<Func<T, object>>> columnsMap)
		{
			if (string.IsNullOrWhiteSpace(orderBy) || columnsMap == null)
			{
				return source;
			}

			var orderByList = orderBy.Split(",", StringSplitOptions.RemoveEmptyEntries);
			foreach (var orderByPart in orderByList)
			{
				var trimmedOrderByPart = orderByPart.Trim();
				var orderDescending = trimmedOrderByPart.EndsWith(" desc");
				var propertyName = orderDescending ? trimmedOrderByPart[0..^5] : trimmedOrderByPart;

				if (!columnsMap.ContainsKey(propertyName))
				{
					continue; // or throw an exception
				}

				source = orderDescending
					? source.OrderByDescending(columnsMap[propertyName])
					: source.OrderBy(columnsMap[propertyName]);
			}

			return source;
		}

		public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> source, int page, int pageSize)
		{
			return source.Skip((page - 1) * pageSize).Take(pageSize);
		}
	}
}
