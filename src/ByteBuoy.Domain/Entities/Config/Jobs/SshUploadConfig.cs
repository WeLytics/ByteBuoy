namespace ByteBuoy.Domain.Entities.Config.Jobs
{
	public class SshUploadConfig : JobConfig
	{
		public string? Host { get; set; }
		public int? Port { get; set; }
		public int? Timeout { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? PrivateKeyPath { get; set; }
		public List<string> Sources { get; set; } = [];
		public List<string> Targets { get; set; } = [];
	}
}
