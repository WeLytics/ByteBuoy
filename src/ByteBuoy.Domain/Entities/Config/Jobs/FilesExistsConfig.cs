namespace ByteBuoy.Domain.Entities.Config.Jobs
{
	public class FilesExistsConfig : JobConfig
	{
		public List<string> Paths { get; set; } = [];
	}
}
