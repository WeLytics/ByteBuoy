using System.Text.Json;
using ByteBuoy.Application.Contracts;
using FluentValidation;

namespace ByteBuoy.Application.Validators
{
	public class CreatePageMetricValidator : AbstractValidator<CreatePageMetricContract>
	{
		public CreatePageMetricValidator()
		{
			RuleFor(x => x.Status).NotEmpty();

			RuleFor(x => x.MetaJson)
					   .Must(BeValidJson<dynamic>)
							.WithMessage("MetaJson is not in a valid format.");


		}

		private static bool BeValidJson<T>(string? json)
		{
			if (string.IsNullOrWhiteSpace(json))
				return true;

			try
			{
				JsonSerializer.Deserialize<T>(json);
				return true; 
			}
			catch (JsonException exJson)
			{
				Console.WriteLine(exJson);
				return false; 
			}
		}
	}

}
