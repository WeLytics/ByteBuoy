namespace ByteBuoy.Domain.Entities.Config.Actions
{
	public class FileExistsConfig : ActionConfig
	{
		public List<string> Sources { get; set; } = [];
		public List<string> Targets { get; set; } = [];

	}
}
