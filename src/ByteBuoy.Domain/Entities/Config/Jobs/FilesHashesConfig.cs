namespace ByteBuoy.Domain.Entities.Config.Jobs
{
	public class FilesHashesConfig : JobConfig
	{
		public List<string> Paths { get; set; } = [];
		public string? HashAlgorithm { get; set; } = "CRC32";
	}
}
