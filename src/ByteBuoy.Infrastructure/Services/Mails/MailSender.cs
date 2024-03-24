using ByteBuoy.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ByteBuoy.Infrastructure.Services.Mails
{


	public class EmailSender : IEmailSender<ApplicationUser>
	{
		public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
		{
			return Task.CompletedTask;
		}

		public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
		{
			throw new NotImplementedException();
		}

		public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
		{
			throw new NotImplementedException();
		}
	}

}
