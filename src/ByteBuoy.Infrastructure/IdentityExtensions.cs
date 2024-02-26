using ByteBuoy.Application.DTO;
using Microsoft.AspNetCore.Identity;

namespace ByteBuoy.Infrastructure
{
	internal static class IdentityExtensions
	{
		public static List<UserError> ToUserErrorList(this IdentityResult result)
		{
			var errors = result.Errors.Select(e => new UserError(e.Description, e.Code));
			return errors.ToList();
		}	
	}
}
