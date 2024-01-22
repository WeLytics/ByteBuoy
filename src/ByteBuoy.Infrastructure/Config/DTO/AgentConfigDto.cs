using ByteBuoy.Domain.Entities.Config.Jobs;

namespace ByteBuoy.Infrastructure.Config
{
	public class AgentConfigDto
	{
		public decimal Version { get; set; }
		public required string Host { get; set; }
		public required string ApiKey { get; set; }
		public required string PageId { get; set; }
		public required List<JobConfigDto> Jobs { get; set; }
	}
}
