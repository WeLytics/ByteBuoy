namespace ByteBuoy.Domain.Entities.Config.Actions
{
	public class FilesCopyConfig : ActionConfig
	{
		public List<string> Sources { get; set; } = [];
		public List<string> Targets { get; set; } = [];
	}
}
