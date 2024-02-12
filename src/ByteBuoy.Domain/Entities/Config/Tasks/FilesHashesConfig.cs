namespace ByteBuoy.Domain.Entities.Config.Tasks
{
	public class FilesHashesConfig : TaskConfig
	{
		public List<string> Paths { get; set; } = [];
		public string? HashAlgorithm { get; set; } = "CRC32";
	}
}
