using ByteBuoy.Application.DTO;
using ByteBuoy.Application.ServiceInterfaces;
using ByteBuoy.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ByteBuoy.Infrastructure.Services
{
	public class IdentityService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) : IIdentityService
	{
		private readonly UserManager<ApplicationUser> _userManager = userManager;
		private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
		private const string _adminRoleName = "admin";

		public async Task<IdentityServiceResult> CreateAdminUser(string username, string password)
		{
			await CreateAdminRoleIfNotExists();

			var adminUser = new ApplicationUser { UserName = username, Email = username };
			var result = await _userManager.CreateAsync(adminUser, password);

			if (!result.Succeeded)
				return new IdentityServiceResult(result.ToUserErrorList());

			await _userManager.AddToRoleAsync(adminUser, _adminRoleName);
			return new IdentityServiceResult() { Succeeded = true };
		}

		internal async Task CreateAdminRoleIfNotExists()
		{
			if (!await _roleManager.RoleExistsAsync(_adminRoleName))
				await _roleManager.CreateAsync(new ApplicationRole { Name = _adminRoleName });
		}

		public async Task<IdentityServiceResult> CreateAdminUserIfNotExistFromSystemEnv(IServiceProvider services)
		{
			var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
			var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

			if (await _userManager.Users.AnyAsync())
				return new IdentityServiceResult() { Succeeded = true };

			if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
			{
				Console.WriteLine("No admin email/password provided in the environment variables.");
				return new IdentityServiceResult() { Succeeded = true };
			}

			await CreateAdminRoleIfNotExists();

			return await CreateAdminUser(adminEmail, adminPassword);
		}
	}
}
