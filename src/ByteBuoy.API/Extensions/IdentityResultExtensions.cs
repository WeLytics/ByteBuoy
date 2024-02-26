using Microsoft.AspNetCore.Identity;

namespace ByteBuoy.API.Extensions
{
	public static class IdentityResultExtensions
	{
		public static string ToString(this IdentityResult result)
		{
			var errors = result.Errors.Select(e => e.Description);
			return string.Join(", ", errors);
		}
	}
}
