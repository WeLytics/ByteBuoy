using ByteBuoy.Application.DTO;
using Microsoft.AspNetCore.Identity;

namespace ByteBuoy.Application.ServiceInterfaces
{
	public interface IIdentityService
	{
		Task<IdentityServiceResult> CreateAdminUserIfNotExistFromSystemEnv(IServiceProvider services);
		Task<IdentityServiceResult> CreateAdminUser(string username, string password);	
	}
}
