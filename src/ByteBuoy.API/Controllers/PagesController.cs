using ByteBuoy.API.Extensions;
using ByteBuoy.Application.Contracts;
using ByteBuoy.Application.Helpers;
using ByteBuoy.Application.Mappers;
using ByteBuoy.Domain.Entities;
using ByteBuoy.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
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
		[Authorize]
		public async Task<ActionResult<IEnumerable<Page>>> GetPages()
		{
			return User?.Identity?.IsAuthenticated == true
				? await _context.Pages.Where(r => r.Deleted == null).ToListAsync()
				: await _context.Pages.Where(r => r.Deleted == null && r.IsPublic).ToListAsync();
		}

		// GET: api/v1/pages/5
		[HttpGet("{pageIdOrSlug}")]
		public async Task<ActionResult<Page>> GetPageById(string pageIdOrSlug)
		{
			var page = await _context.GetPageByIdOrSlug(pageIdOrSlug);

			if (page != null && User?.Identity?.IsAuthenticated == false && !page.IsPublic)
				return Unauthorized();

			if (page == null)
				return NotFound();

			return page;
		}

		// POST: api/v1/pages
		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<ActionResult<Metric>> CreatePage([FromBody] CreatePageContract createPage)
		{
			var page = new PageContractMappers().CreatePageDtoToPage(createPage);
			if (string.IsNullOrEmpty(page.Slug))
				page.Slug = CreatePageTitleSlug(page.Title);

			_context.Pages.Add(page);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetPageById", new { pageIdOrSlug = page.Id }, page);
		}

		// DELETE: api/v1/pages/
		[HttpDelete("{pageIdOrSlug}")]
		[Authorize(Roles = "admin")]
		public async Task<ActionResult> DeletePage([FromRoute] string pageIdOrSlug)
		{
			var page = await _context.GetPageBySlug(pageIdOrSlug);

			if (page == null)
				return NotFound();

			_context.Pages.Remove(page);
			await _context.SaveChangesAsync();

			return NoContent();
		}


		private string CreatePageTitleSlug(string title)
		{
			string baseSlug = SlugGenerator.GenerateSlug(title);
			string finalSlug = baseSlug;
			int counter = 1;

			// Check if the slug already exists in the database
			while (_context.Pages.Any(e => e.Slug == finalSlug))
			{
				finalSlug = $"{baseSlug}-{counter++}";
			}

			return finalSlug;
		}



		// GET: api/v1/pages/5/badge
		[HttpGet("{pageIdOrSlug}/badge")]
		public async Task<IActionResult> GetPageBadge(string pageIdOrSlug)
		{
			var page = await _context.GetPageByIdOrSlug(pageIdOrSlug);

			if (page == null)
				return NotFound();

			var svgContent = Utilities.PageMetricBadgeGenerator.GenerateBadge(page);
			return Content(svgContent, "image/svg+xml");
		}


		// POST: api/v1/pages/5/visibility
		[HttpPost("{pageIdOrSlug}/visibility")]
		[Authorize(Roles ="admin")]
		public async Task<IActionResult> SetPageVisiblity([FromBody] UpdatePageVisibility updatePageVisibility, string pageIdOrSlug)
		{
			var page = await _context.GetPageByIdOrSlug(pageIdOrSlug);

			if (page == null)
				return NotFound();

			page.IsPublic = updatePageVisibility.IsPublic;
			await _context.SaveChangesAsync();
			return NoContent();
		}
	}
}
