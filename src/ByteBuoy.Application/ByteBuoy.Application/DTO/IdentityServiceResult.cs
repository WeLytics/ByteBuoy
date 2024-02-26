namespace ByteBuoy.Application.DTO
{
	public record IdentityServiceResult
	{
		public IdentityServiceResult() { }
		public IdentityServiceResult(List<UserError> errors) => Errors.AddRange(errors);

		public IdentityServiceResult(string errorMessage) => Errors =
			[
				 new UserError(errorMessage)
			];

		public bool Succeeded { get; set; }
		public List<UserError> Errors { get; internal set; } = [];
	}
}
