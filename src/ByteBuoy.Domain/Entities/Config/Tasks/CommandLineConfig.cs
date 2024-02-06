namespace ByteBuoy.Domain.Entities.Config.Tasks
{
	public class CommandLineConfig : TaskConfig
	{
		public List<string> Commands { get; set; } = [];
		public string? WorkingDirectory { get; set; }
	}
}
