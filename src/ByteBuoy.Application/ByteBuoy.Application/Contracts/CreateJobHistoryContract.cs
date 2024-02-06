namespace ByteBuoy.Application.Contracts
{
	public class CreateJobHistoryContract
	{
		public int JobId { get; set; }
		public int TaskNumber { get; set; }
		public string? TaskName { get; set; }
		public string? Description { get; set; }

		public Domain.Enums.TaskStatus Status { get; set; }
		public string? ErrorMessage { get; set; }
	}
}
