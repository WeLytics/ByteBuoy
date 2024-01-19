namespace ByteBuoy.Domain.Entities
{
	public class MetricCategory
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public string? Slug { get; set; }
	}
}
