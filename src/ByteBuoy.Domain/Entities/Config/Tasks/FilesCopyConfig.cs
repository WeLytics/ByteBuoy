namespace ByteBuoy.Domain.Entities.Config.Tasks
{
	public class FilesCopyConfig : TaskConfig
	{
		public List<string> Sources { get; set; } = [];
		public List<string> Targets { get; set; } = [];
	}
}
