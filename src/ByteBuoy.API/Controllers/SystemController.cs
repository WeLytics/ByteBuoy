using ByteBuoy.Application.Contracts;
using ByteBuoy.Application.Helpers;
using ByteBuoy.Application.ServiceInterfaces;
using ByteBuoy.Domain.Entities;
using ByteBuoy.Domain.Entities.Identity;
using ByteBuoy.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteBuoy.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiExplorerSettings(GroupName = "V1")]
	[ApiController]
	public class SystemController(ByteBuoyDbContext dbContext, IIdentityService identityService) : ControllerBase
	{
		private readonly ByteBuoyDbContext _dbContext = dbContext;
		private readonly IIdentityService _identityService = identityService;

		private async Task<bool> IsFirstRun()
		{
			return !(await _dbContext.Users.AnyAsync() && await _dbContext.Pages.AnyAsync());
		}

		[HttpGet("isFirstRun")]
		public async Task<IActionResult> GetIsFirstRun()
		{
			if (await IsFirstRun())			
				return Ok(true);	

			return Ok(false);
		}


		[HttpPost("initialSetup")]
		public async Task<IActionResult> InitialSetup([FromBody] CreateSystemSetupContract contract)
		{
			if (string.IsNullOrEmpty(contract.AdminEmail) || string.IsNullOrEmpty(contract.AdminPassword) || string.IsNullOrEmpty(contract.PageTitle))
				return BadRequest("AdminEmail, AdminPassword and PageName are required");

			if (!await IsFirstRun())
				return BadRequest("System already set up");

			var result = await _identityService.CreateAdminUser(contract.AdminEmail, contract.AdminPassword);

			if (!result)
				return BadRequest("Failed to create admin user");

			var page = new Page
			{
				Title = contract.PageTitle,
				Slug = SlugGenerator.GenerateSlug(contract.PageTitle),
				IsPublic = true,
			};
			await _dbContext.Pages.AddAsync(page);

			return Ok(true);
		}
	}
}
