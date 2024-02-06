using ByteBuoy.Domain.Entities;
using ByteBuoy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ByteBuoy.API.Extensions
{
	public static class DbContextExtensions
	{
		public static async Task<Page?> GetPageBySlug(this ByteBuoyDbContext context, string slug)
		{
			return await context.Pages.SingleOrDefaultAsync(r => r.Slug == slug);
		}

		public static async Task<Page?> GetPageByIdOrSlug(this ByteBuoyDbContext context, string id)
		{
			if (int.TryParse(id, out int pageId))
				return await context.Pages.SingleOrDefaultAsync(r => r.Id == pageId);

			return await context.GetPageBySlug(id);
		}

		public static async Task<Page?> GetPageById(this ByteBuoyDbContext context, int pageId)
		{
			return await context.Pages.SingleOrDefaultAsync(r => r.Id == pageId);
		}

		public static async Task<Job?> GetJobById(this ByteBuoyDbContext context, int jobId)
		{
			return await context.Jobs.SingleOrDefaultAsync(r => r.Id == jobId);
		}
	}
}
