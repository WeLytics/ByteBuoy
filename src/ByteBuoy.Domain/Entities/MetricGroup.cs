namespace ByteBuoy.Domain.Entities
{
	public class MetricGroup
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public string? Description { get; set; }
	}
}
