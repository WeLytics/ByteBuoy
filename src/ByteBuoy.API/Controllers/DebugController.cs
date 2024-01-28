using ByteBuoy.Application.Contracts;
using ByteBuoy.Application.Mappers;
using ByteBuoy.Domain.Entities;
using ByteBuoy.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteBuoy.API.Controllers
{

	[Route("[controller]")]
	[ApiController]
	public class DebugController(ByteBuoyDbContext context) : ControllerBase
	{
		private readonly ByteBuoyDbContext _context = context;

		// GET: /debug
		[HttpGet]
		public ActionResult<IEnumerable<Page>> GetIndex()
		{
			// returns the provided http headers from the client
			return Ok(Request.Headers);
		}

		// GET: api/v1/pages/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Page>> GetPageById(int id)
		{
			var page = await _context.Pages.FindAsync(id);

			if (page == null)
			{
				return NotFound();
			}

			return page;
		}

		// POST: api/v1/pages
		[HttpPost]
		public async Task<ActionResult<Metric>> PostPage(CreatePageContract createPage)
		{
			var page = new PageContractMappers().CreatePageDtoToPage(createPage);
			_context.Pages.Add(page);
			await _context.SaveChangesAsync();

			return CreatedAtAction("PostMetric", new { id = page.Id }, page);
		}
	}
}
