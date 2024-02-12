namespace ByteBuoy.Infrastructure.Config
{
	public class TaskDto
	{
		public required string Name { get; set; }
		public required string Action { get; set; }
		public bool? ContinueOnError { get; set; }
		public bool? Overwrite { get; set; } //for filesCopy@v1 and filesMove@v1
		public List<string>? Commands { get; set; } // for commandLine@v1
		public string? WorkingDirectory { get; set; } // for commandLine@v1
		public List<string>? Sources { get; set; } 
		public List<string>? Targets { get; set; } 
		public List<string>? Paths { get; set; } // for filesHashes@v1, filesExists@v1
		public string? HashAlgorithm { get; set; } // for filesHashes@v1
		public Dictionary<string, string>? Labels { get; set; }

		// for sshUpload@v1
		public string? Host { get; set; } 
		public int? Port { get; set; }
		public int? Timeout { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? PrivateKeyPath { get; set; }
	}
}
