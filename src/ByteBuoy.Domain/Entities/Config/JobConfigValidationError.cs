namespace ByteBuoy.Domain.Entities.Config
{
	public class JobConfigValidationError(string location, string error)
	{
		public string Location { get; set; } = location;
		public string Error { get; set; } = error;
	}
}
