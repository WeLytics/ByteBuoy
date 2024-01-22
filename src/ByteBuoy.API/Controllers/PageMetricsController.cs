using ByteBuoy.API.Extensions;
using ByteBuoy.Application.Contracts;
using ByteBuoy.Application.Mappers;
using ByteBuoy.Domain.Entities;
using ByteBuoy.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteBuoy.API.Controllers
{

	[Route("api/v1/pages/{PageId}/metrics")]
	[ApiExplorerSettings(GroupName = "V1")]
	[ApiController]
	public class PageMetricsController(ByteBuoyDbContext context) : ControllerBase
	{
		private readonly ByteBuoyDbContext _context = context;

		// GET: api/v1/pages/pageId/metrics
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Metric>>> GetPageMetrics(int pageId)
		{
			return await _context.Metrics.Where(r => r.Page.Id == pageId).ToListAsync();
		}
				
		// GET: api/v1/pages/pageId/metrics/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Metric>> GetPageMetricById(int pageId, int metricId)
		{
			var pageMetric = await _context.Metrics.SingleOrDefaultAsync(r => r.Id == metricId && r.Page.Id == pageId);

			if (pageMetric == null)
			{
				return NotFound();
			}

			return pageMetric;
		}

		// POST: api/v1/pages/pageId/metrics
		[HttpPost]
		public async Task<ActionResult<Metric>> PostMetric(CreatePageMetricContract createPageMetric)
		{
			var page = await _context.GetPageById(createPageMetric.PageIdOrSlug);
			if (page == null)
				return NotFound();

			var pageMetric = new PageContractMappers().CreatePageMetricDtoToPageMetric(createPageMetric);
			pageMetric.Page = page;
			_context.Metrics.Add(pageMetric);
			await _context.SaveChangesAsync();

			return CreatedAtAction("PostMetric", new { id = pageMetric.Id }, pageMetric);
		}
	}
}
