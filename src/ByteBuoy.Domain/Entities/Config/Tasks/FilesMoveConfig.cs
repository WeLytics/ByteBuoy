namespace ByteBuoy.Domain.Entities.Config.Tasks
{
	public class FilesMoveJobConfig : TaskConfig
	{
		public List<string> Sources { get; set; } = [];
		public List<string> Targets { get; set; } = [];
	}
}
