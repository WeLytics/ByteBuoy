using ByteBuoy.Domain.Entities;
using FluentValidation;

namespace ByteBuoy.Application.Validators
{
	public class MetricValidator : AbstractValidator<Metric>
	{
		public MetricValidator()
		{
			RuleFor(x => x.Page).NotEmpty();
			RuleFor(x => x.Created).NotEmpty();
			RuleFor(x => x.Status).NotEmpty();
		}
	}

}
