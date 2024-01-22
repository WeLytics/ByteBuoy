namespace ByteBuoy.Domain.Entities.Config.Jobs
{
	public class FilesExistsConfig : JobConfig
	{
		public List<string> Sources { get; set; } = [];
		public List<string> Targets { get; set; } = [];

	}
}
