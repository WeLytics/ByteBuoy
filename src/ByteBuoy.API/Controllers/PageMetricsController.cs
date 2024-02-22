using ByteBuoy.API.Extensions;
using ByteBuoy.Application.Contracts;
using ByteBuoy.Application.Mappers;
using ByteBuoy.Application.ServiceInterfaces;
using ByteBuoy.Domain.Entities;
using ByteBuoy.Infrastructure.Data;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteBuoy.API.Controllers
{

	[Route("api/v1/pages/{pageIdOrSlug}/metrics")]
	[ApiExplorerSettings(GroupName = "V1")]
	[ApiController]
	public class PageMetricsController(ByteBuoyDbContext context, IMetricsConsolidationService metricsConsolidationService,
		IValidator<CreatePageMetricContract> validator, ILogger<PageMetricsController> logger) : ControllerBase
	{
		private readonly ByteBuoyDbContext _context = context;
		private readonly IMetricsConsolidationService _metricsConsolidationService = metricsConsolidationService;
		private readonly IValidator<CreatePageMetricContract> _validator = validator;
		private readonly ILogger<PageMetricsController> _logger = logger;

		// GET: api/v1/pages/{pageIdOrSlug}/metrics
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Metric>>> GetPageMetrics([FromRoute] string pageIdOrSlug)
		{
			var page = await _context.GetPageByIdOrSlug(pageIdOrSlug);
			if (page == null)
				return NotFound();

			return await _context.Metrics.Where(r => r.Page == page)
										 .OrderByDescending(r => r.Created)
										 .ToListAsync();
		}

		// GET: api/v1/pages/{pageIdOrSlug}/metrics/consolidated
		[HttpGet("consolidated")]
		public async Task<ActionResult<PageMetricConsolidationDto>> GetPageConsolidated([FromRoute] string pageIdOrSlug)
		{
			var page = await _context.GetPageByIdOrSlug(pageIdOrSlug);
			if (page == null)
				return NotFound();

			try
			{
				return await _metricsConsolidationService.ConsolidateMetricsAsync(page);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message + ex.StackTrace);
				throw;
			}
		}

		// GET: api/v1/pages/{pageIdOrSlug}/metrics/groups
		[HttpGet("groups")]
		public async Task<ActionResult<IEnumerable<MetricGroup>>> GetPageMetricGroups([FromRoute] string pageIdOrSlug)
		{
			var page = await _context.GetPageByIdOrSlug(pageIdOrSlug);
			if (page == null)
				return NotFound();

			var metricGroups = await _context.MetricGroups.Where(r => r.Page == page).ToListAsync();

			if (metricGroups == null)
				return NotFound();

			return metricGroups;
		}

		// PATCH: api/v1/pages/{pageIdOrSlug}/metrics/groups
		[HttpPatch("groups/{groupId}")]
		public async Task<ActionResult<MetricGroup>> UpdatePageMetricsGroup([FromRoute] string pageIdOrSlug, [FromRoute] int groupId,
																					     [FromBody] UpdatePageMetricGroupContract updateContract)
		{
			var page = await _context.GetPageByIdOrSlug(pageIdOrSlug);
			if (page == null)
				return NotFound();

			var metricGroup = await _context.MetricGroups.SingleOrDefaultAsync(r => r.Page == page && r.Id == groupId);
			if (metricGroup == null)
				return NotFound();


			if (updateContract.Title != null)
				metricGroup.Title = updateContract.Title;

			if (updateContract.Description != null)
				metricGroup.Description = updateContract.Description;

			if (updateContract.MetricInterval != null)
				metricGroup.MetricInterval = updateContract.MetricInterval.Value;

			if (updateContract.GroupBy != null)
				metricGroup.GroupBy = updateContract.GroupBy;

			await _context.SaveChangesAsync();

			return Ok(metricGroup);
		}

		// GET: api/v1/pages/{pageIdOrSlug}/metrics/5
		[HttpGet("{metricId}")]
		public async Task<ActionResult<Metric>> GetPageMetricById([FromRoute] string pageIdOrSlug, [FromRoute] int metricId)
		{
			var page = await _context.GetPageByIdOrSlug(pageIdOrSlug);
			if (page == null)
				return NotFound();

			var pageMetric = await _context.Metrics.Include(r => r.MetricGroup)
												   .SingleOrDefaultAsync(r => r.Id == metricId && r.Page == page);

			if (pageMetric == null)
				return NotFound();

			return pageMetric;
		}

		// POST: api/v1/pages/{pageIdOrSlug}/metrics
		[HttpPost]
		public async Task<IActionResult> PostMetric(CreatePageMetricContract createPageMetric, [FromRoute] string pageIdOrSlug)
		{
			var page = await _context.GetPageByIdOrSlug(pageIdOrSlug);
			if (page == null)
				return NotFound();

			var result = await _validator.ValidateAsync(createPageMetric);
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
			page.Updated = DateTime.UtcNow;
			page.PageStatus = createPageMetric.Status;
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetPageMetricById", new { pageIdOrSlug, metricId = pageMetric.Id }, pageMetric);
		}
	}
}
