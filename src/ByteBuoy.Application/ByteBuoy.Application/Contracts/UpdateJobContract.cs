using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Application.Contracts
{
	public class UpdateJobContract
	{
		public int JobId { get; set; }
		public JobStatus Status { get; set; }
		public string? Description { get; set; }
	}
}
