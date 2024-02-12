using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Domain.Entities
{
	public class Job
    {
        public int Id { get; set; }
        public string? Description { get; set; }
		public string? HostName { get; set; }
		public JobStatus Status { get; set; }
		public DateTime? StartedDateTime { get; set; }
		public DateTime? FinishedDateTime { get; set; }
    }
}
