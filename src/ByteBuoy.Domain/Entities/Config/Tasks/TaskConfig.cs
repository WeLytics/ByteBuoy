namespace ByteBuoy.Domain.Entities.Config.Tasks
{
	public abstract class TaskConfig
	{
		public required string Name { get; set; }
		public required string Action { get; set; }
		public string? Description { get; set; }
		public bool? ContinueOnError { get; set; }
		public Dictionary<string, string>? Labels { get; set; }
	}
}
