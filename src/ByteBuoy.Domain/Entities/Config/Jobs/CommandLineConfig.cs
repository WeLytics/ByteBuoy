namespace ByteBuoy.Domain.Entities.Config.Jobs
{
	public class CommandLineConfig : JobConfig
	{
		public List<string> Commands { get; set; } = [];
		public string? WorkingDirectory { get; set; }
	}
}
