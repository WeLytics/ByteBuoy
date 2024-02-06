namespace ByteBuoy.Infrastructure.Config
{
	public class JobDto
	{
		public decimal Version { get; set; }
		public required string Host { get; set; }
		public required string ApiKey { get; set; }
		public required string Page { get; set; }

		public string? Description { get; set; }
		public required List<TaskDto> Tasks { get; set; }
	}
}
