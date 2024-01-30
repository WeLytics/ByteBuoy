using ByteBuoy.API.Extensions;
using ByteBuoy.Application.Contracts;
using ByteBuoy.Application.Mappers;
using ByteBuoy.Domain.Entities;
using ByteBuoy.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ByteBuoy.API.Controllers
{

	[Route("api/v1/pages/{pageIdOrSlug}/metrics")]
	[ApiExplorerSettings(GroupName = "V1")]
	[ApiController]
	public class PageMetricsController(ByteBuoyDbContext context) : ControllerBase
	{
		private readonly ByteBuoyDbContext _context = context;

		// GET: api/v1/pages/{pageIdOrSlug}/metrics
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Metric>>> GetPageMetrics([FromRoute] string pageIdOrSlug)
		{
			var page = await _context.GetPageByIdOrSlug(pageIdOrSlug);
			if (page == null)
				return NotFound();

			return await _context.Metrics.Where(r => r.Page == page).ToListAsync();
		}

		// GET: api/v1/pages/{pageIdOrSlug}/metrics/5
		[HttpGet("{metricId}")]
		public async Task<ActionResult<Metric>> GetPageMetricById([FromRoute] string pageIdOrSlug, [FromRoute] int metricId)
		{
			var page = await _context.GetPageByIdOrSlug(pageIdOrSlug);
			if (page == null)
				return NotFound();

			var pageMetric = await _context.Metrics.SingleOrDefaultAsync(r => r.Id == metricId && r.Page == page);

			if (pageMetric == null)
			{
				return NotFound();
			}

			return pageMetric;
		}

		// POST: api/v1/pages/{pageIdOrSlug}/metrics
		[HttpPost]
		public async Task<ActionResult<Metric>> PostMetric(CreatePageMetricContract createPageMetric, [FromRoute] string pageIdOrSlug)
		{
			var page = await _context.GetPageByIdOrSlug(pageIdOrSlug);
			if (page == null)
				return NotFound();

			var pageMetric = new PageContractMappers().CreatePageMetricDtoToPageMetric(createPageMetric);
			pageMetric.Page = page;


			if (createPageMetric.MetricGroupId == null)
			{
				var metricGroup = await _context.MetricGroups.SingleOrDefaultAsync(r => r.Page.Id == page.Id);
				metricGroup ??= new MetricGroup() { Page = page, Title = "Default Metric Group" };

				if (_context.Entry(metricGroup).State == EntityState.Detached)
					_context.MetricGroups.Add(metricGroup);
				pageMetric.MetricGroup = metricGroup;
			}
			else
			{
				var metricGroup = await _context.MetricGroups.SingleOrDefaultAsync(r => r.Id == createPageMetric.MetricGroupId);
				if (metricGroup == null)
					return NotFound();

				pageMetric.MetricGroup = metricGroup;
			}


			_context.Metrics.Add(pageMetric);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetPageMetricById", new { pageIdOrSlug, metricId = pageMetric.Id }, pageMetric);
		}
	}
}
