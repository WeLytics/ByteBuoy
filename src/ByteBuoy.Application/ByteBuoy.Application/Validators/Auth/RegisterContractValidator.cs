using ByteBuoy.Application.Contracts.Auth;
using FluentValidation;

namespace ByteBuoy.Application.Validators.Auth
{
	public class RegisterContractValidator : AbstractValidator<RegisterContract>
	{
		public RegisterContractValidator()
		{
			RuleFor(x => x.Email).NotEmpty();
			RuleFor(x => x.Password).NotEmpty();
		}
	}
}
