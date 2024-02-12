namespace ByteBuoy.Domain.Entities
{
	public class Page
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public string? Slug { get; set; }

		public bool IsPublic { get; set; }
		public DateTime Created { get; set; } = DateTime.UtcNow;
		public DateTime? Updated { get; set; }
		public DateTime? Deleted { get; set; }

		public IList<MetricGroup> MetricGroups { get; set; } = new List<MetricGroup>();
	}
}
