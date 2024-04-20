using ByteBuoy.Application.Contracts.Auth;
using ByteBuoy.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuoy.API.Controllers
{

	[Route("api/v1/auth")]
	[ApiExplorerSettings(GroupName = "V1")]
	[ApiController]
	public class AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager = userManager;
		private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginContract model)
		{
			var user = await _userManager.FindByNameAsync(model.Email);
			if (user != null)
			{
				var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
				if (result.Succeeded)
				{
					var userRoles = await _userManager.GetRolesAsync(user);

					return Ok(new
					{
						IsAuthorized = true,
						User = new { user.Id, user.Email },
						UserRoles = userRoles
					});
				}
			}

			return Unauthorized();
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterContract model)
		{
			var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
				return BadRequest(new { errors = result });

			return Ok();
		}


		[Authorize]
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok();
		}
	}
}
