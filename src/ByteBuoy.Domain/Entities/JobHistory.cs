namespace ByteBuoy.Domain.Entities
{
	public class JobHistory
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int TaskNumber { get; set; }
        public string? TaskName { get; set; }
        public string? Description { get; set; }
		public Enums.TaskStatus Status { get; set; }
		public DateTime CreatedDateTime { get; set; }

		public string? ErrorMessage { get; set; }
	}
}
