namespace ByteBuoy.Domain.Entities.Config
{
	public abstract class ActionConfig
    {
		public required string Name { get; set; }
        public string? Description { get; set; }
		public bool? ContinueOnError { get; set; }
		public List<Dictionary<string, string>>? Labels { get; set; }
	}
}
