namespace ByteBuoy.Domain.Entities.Config.Actions
{
	public class BashCommands : ActionConfig
	{
		public List<string> Commands { get; set; } = [];
		public string? WorkingDirectory { get; set; }
	}
}
