using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Application.Contracts
{
	public class CreateJobRunContract
	{
		public string? Description { get; set; }
		public string? HostName { get; set; }
	}
}
