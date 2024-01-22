namespace ByteBuoy.Domain.Entities.Config.Jobs
{
	public abstract class JobConfig
	{
		public required string Name { get; set; }
		public required string Action { get; set; }
		public string? Description { get; set; }
		public bool? ContinueOnError { get; set; }
		public List<Dictionary<string, string>>? Labels { get; set; }
	}
}
