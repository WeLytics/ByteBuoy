namespace ByteBuoy.Domain.Entities
{
	public class Page
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public string? Slug { get; set; }
		public required DateTime Created { get; set; }
		public DateTime? Updated { get; set; }
		public DateTime? Deleted { get; set; }
	}
}
