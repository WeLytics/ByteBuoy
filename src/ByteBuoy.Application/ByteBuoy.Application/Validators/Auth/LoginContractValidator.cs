using ByteBuoy.Application.Contracts.Auth;
using FluentValidation;

namespace ByteBuoy.Application.Validators.Auth
{
	public class LoginContractValidator : AbstractValidator<LoginContract>
	{
		public LoginContractValidator()
		{
			RuleFor(x => x.Email).NotEmpty();
			RuleFor(x => x.Password).NotEmpty();
		}
	}
}
