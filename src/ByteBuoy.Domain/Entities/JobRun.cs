using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Domain.Entities
{
	public class JobRun
    {
        public int Id { get; set; }
        public string? Description { get; set; }
		public JobStatus JobStatus { get; set; }
		public DateTime JobStarted { get; set; }
		public DateTime JobFinished { get; set; }
    }
}
