namespace ByteBuoy.Domain.Entities.Config.Tasks
{
	public class FilesExistsConfig : TaskConfig
	{
		public List<string> Paths { get; set; } = [];
	}
}
