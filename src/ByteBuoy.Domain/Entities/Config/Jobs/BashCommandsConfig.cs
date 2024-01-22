namespace ByteBuoy.Domain.Entities.Config.Jobs
{
	public class BashCommands : JobConfig
	{
		public List<string> Commands { get; set; } = [];
		public string? WorkingDirectory { get; set; }
	}
}
