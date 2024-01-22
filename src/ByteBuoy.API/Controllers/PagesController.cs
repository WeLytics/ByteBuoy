using ByteBuoy.Application.Contracts;
using ByteBuoy.Application.Mappers;
using ByteBuoy.Domain.Entities;
using ByteBuoy.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteBuoy.API.Controllers
{

	[Route("api/v1/[controller]")]
	[ApiExplorerSettings(GroupName = "V1")]
	[ApiController]
	public class PagesController(ByteBuoyDbContext context) : ControllerBase
	{
		private readonly ByteBuoyDbContext _context = context;

		// GET: api/v1/pages
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Page>>> GetPages()
		{
			return await _context.Pages.ToListAsync();
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
