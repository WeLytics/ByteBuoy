namespace ByteBuoy.Domain.Entities.Identity
{
	public class ApplicationRole : Microsoft.AspNetCore.Identity.IdentityRole<Guid>
	{
		public ApplicationRole()
		{
			
		}
		public ApplicationRole(string roleName) : base(roleName)
		{
		}
	}
}
