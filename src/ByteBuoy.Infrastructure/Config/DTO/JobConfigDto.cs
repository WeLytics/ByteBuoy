namespace ByteBuoy.Infrastructure.Config
{
	public class JobConfigDto
	{
		public string Name { get; set; }
		public string Action { get; set; }
		public bool? ContinueOnError { get; set; }
		public List<string> Commands { get; set; } // for bashCommand@v1
		public List<string> Sources { get; set; } // for sftpConnection@v1
		public List<string> Targets { get; set; } // for sftpConnection@v1
		public string SftpHost { get; set; } // for sftpConnection@v1
		public string Username { get; set; } // for sftpConnection@v1
		public List<string> Paths { get; set; } // for checkHashes@v1, checkFiles@v1
		public string WorkingDirectory { get; set; } // for bashCommands@v1
		public Dictionary<string, string> Labels { get; set; }
	}
}
