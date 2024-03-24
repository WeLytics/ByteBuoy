namespace ByteBuoy.Application.DTO
{
	public class UserError
	{
		public UserError()
		{
		}

		public UserError(string message)
		{
			Message = message;
		}

		public UserError(string message, string code)
		{
			Message = message;
			Code = code;
		}

		public UserError(string message, string code, string? i18nKey, string? field)
		{
			Message = message;
			Code = code;
			I18nKey = i18nKey;
			Field = field;
		}

		public string? I18nKey { get; set; }
		public string? Field { get; set; }
		public string Message { get; set; } = null!;
		public string? Code { get; set; }


		public override string ToString() => $"{Message}";
	}
}
