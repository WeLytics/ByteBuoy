namespace ByteBuoy.Application.ServiceInterfaces
{
	public interface IIdentityService
	{
		Task<bool> CreateAdminUserIfNotExistFromSystemEnv(IServiceProvider services);
		Task<bool> CreateAdminUser(string username, string password);	
	}
}
