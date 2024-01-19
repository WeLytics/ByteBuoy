namespace ByteBuoy.Domain.Entities
{
	public class Incident	
	{
		public int Id { get; set; }
		public required Page Page { get; set; }
		public required string Title { get; set; }

		public required DateTime Created { get; set; }
		public DateTime? Updated { get; set; }
		public DateTime? Deleted { get; set; }
	}
}
