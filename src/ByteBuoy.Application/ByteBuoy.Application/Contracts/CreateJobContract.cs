using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Application.Contracts
{
	public class CreateJobContract
	{
		public string? Description { get; set; }
		public string? HostName { get; set; }
		public JobStatus Status { get; set; }
	}
}
