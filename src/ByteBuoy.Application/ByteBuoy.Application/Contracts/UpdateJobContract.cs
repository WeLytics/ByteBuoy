using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Application.Contracts
{
	public class UpdateJobContract
	{
		public int JobId { get; set; }
		public DateTime? FinishedDateTime { get; set; }
		public JobStatus Status { get; set; }
	}
}
