using System.Text.Json;
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


			RuleFor(x => x.MetaJson)
					   .Must(BeValidJson<string>)
							.WithMessage("MetaJson is not in a valid format.");


		}

		private static bool BeValidJson<T>(string json)
		{
			try
			{
				JsonSerializer.Deserialize<T>(json);
				return true; 
			}
			catch (JsonException)
			{
				return false; 
			}
		}
	}

}
