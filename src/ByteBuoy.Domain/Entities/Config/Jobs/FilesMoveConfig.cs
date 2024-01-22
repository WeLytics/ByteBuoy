namespace ByteBuoy.Domain.Entities.Config.Jobs
{
	public class FilesMoveJobConfig : JobConfig
	{
		public List<string> Sources { get; set; } = [];
		public List<string> Targets { get; set; } = [];
	}
}
