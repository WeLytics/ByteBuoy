namespace ByteBuoy.Infrastructure.Config
{
	public class AgentConfigDto
	{
		public int Version { get; set; }
		public string Host { get; set; }
		public string Apikey { get; set; }
		public List<JobConfigDto> Jobs { get; set; }
	}
}
