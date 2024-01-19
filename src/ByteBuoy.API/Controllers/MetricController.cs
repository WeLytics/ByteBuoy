using ByteBuoy.Domain.Entities;
using ByteBuoy.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteBuoy.API.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class MetricsController(ByteBuoyDbContext context) : ControllerBase
	{
		private readonly ByteBuoyDbContext _context = context;

		// GET: api/Metrics
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Metric>>> GetMetrics()
		{
			return await _context.Metrics.ToListAsync();
		}

		// GET: api/Metrics/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Metric>> GetMetric(int id)
		{
			var metric = await _context.Metrics.FindAsync(id);

			if (metric == null)
			{
				return NotFound();
			}

			return metric;
		}

		// POST: api/Metrics
		[HttpPost]
		public async Task<ActionResult<Metric>> PostMetric(Metric metric)
		{
			_context.Metrics.Add(metric);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetMetric", new { id = metric.Id }, metric);
		}

		// Other CRUD operations (PUT, DELETE) go here...
	}
}
