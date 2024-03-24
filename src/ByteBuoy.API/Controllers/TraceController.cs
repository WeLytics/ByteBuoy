using ByteBuoy.API.Extensions;
using ByteBuoy.API.Models;
using ByteBuoy.Application.Contracts;
using ByteBuoy.Application.Mappers;
using ByteBuoy.Domain.Entities;
using ByteBuoy.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteBuoy.API.Controllers
{

	[Route("api/v1/trace")]
	[ApiExplorerSettings(GroupName = "V1")]
	[ApiController]
	public class TraceController(ByteBuoyDbContext context) : ControllerBase
	{
		private readonly ByteBuoyDbContext _context = context;

		// GET: api/v1/trace/{fileHash}
		[HttpGet("{fileHash}")]
		public async Task<ActionResult<IEnumerable<Metric>>> GetMetricsByHash([FromRoute] string fileHash)
		{
			return await _context.Metrics.Where(r => r.HashSHA256 == fileHash)
							 .Include(r => r.MetricGroup)
								.ThenInclude(r => r!.Page)
							 .OrderByDescending(r => r.Created)
							 .ToListAsync();
		}
	}
}
